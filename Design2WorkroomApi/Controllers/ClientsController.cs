using AutoMapper;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Enums;
using Design2WorkroomApi.Helpers;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;
using TinifyAPI;

namespace Design2WorkroomApi.Controllers
{

    // TODO
    // versioning
    // https://www.udemy.com/course/quick-introduction-to-aspnet-mvc-core-20/learn/lecture/18079125#overview
    // https://levelup.gitconnected.com/net-5-use-api-versioning-4ffe8b0210b2

    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepo;
        private readonly AppUserHelper _appUserHelper;

        public ClientsController(ILogger<ClientsController> logger, 
            IMapper mapper,
            IClientRepository clientRepo, 
            AppUserHelper appUserHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _clientRepo = clientRepo;
            _appUserHelper = appUserHelper;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _clientRepo.GetAllClientsAsync();

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dtoList = _mapper.Map<List<ClientDto>>(result.Clients);
            return Ok(dtoList);
        }

        [HttpGet("Designer/{designerId:guid}")]
        public async Task<IActionResult> GetClientsByDesignerId(Guid designerId)
        {
            var result = await _clientRepo.GetClientsByDesignerIdAsync(designerId);
            if (!result.IsSuccess || result.Clients is null) return NotFound(result.ErrorMessage);

            var clientsDto = new List<ClientDto>();
            foreach (var client in result.Clients)
            {
                var dto = _mapper.Map<ClientDto>(client);
                var activeStatusResult = await _clientRepo.GetDesignerClientActiveStatusAsync(designerId, client.Id);
                dto.Active = activeStatusResult.IsActive;
                dto.Profile.ProfilePicUrl = $"https://avatars.dicebear.com/api/avataaars/{client.Profile.FirstName}.svg";
                clientsDto.Add(dto);
            }

            return Ok(clientsDto);
        }

        [HttpGet("{id:guid}", Name = "GetClient")]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var result = await _clientRepo.GetClientByIdAsync(id);

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dto = _mapper.Map<ClientDto>(result.Client);
            dto.Profile.ProfilePicUrl = $"https://avatars.dicebear.com/api/avataaars/{dto.Profile.FirstName}.svg";
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreateDto clientCreateDto)
        {
            var existsResult = await _clientRepo.ClientExistsAsync(clientCreateDto.UserName);

            if (existsResult.Exists)
            {
                ModelState.AddModelError("", "Username already exists");
                return StatusCode(403, ModelState);
            }
            var client = _mapper.Map<ClientModel>(clientCreateDto);
            client.AppUserRole = _appUserHelper.GetAppUserRole(clientCreateDto.UserRole);
            client.CreatedAt = DateTime.UtcNow;

            //
            /// client.InvitationAccepted
            /// 

            var createResult = await _clientRepo.CreateClientAsync(client);
            if (!createResult.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {client.UserName}");
                return StatusCode(500, ModelState);
            }

            var dto = _mapper.Map<DesignerDto>(client);
            return CreatedAtRoute(nameof(GetClient), new { id = dto.Id }, dto);
        }

        // PUT: api/Client/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateClient(Guid id, ClientUpdateDto clientUpdateDto)
        {
            var getResult = await _clientRepo.GetClientByIdAsync(id);

            if (!getResult.IsSuccess || getResult.Client is null) return NotFound(getResult.ErrorMessage);
            _mapper.Map(clientUpdateDto, getResult.Client);
            getResult.Client.UpdatedAt = DateTime.UtcNow;

            //
            /// client.InvitationAccepted
            /// 

            var updateResult = await _clientRepo.UpdateClientAsync(getResult.Client);
            if (!updateResult.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when updating the record { getResult.Client.UserName }");
            return StatusCode(500, ModelState);
        }

        // DELETE: api/Client/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var existsResult = await _clientRepo.ClientExistsAsync(id);

            if (!existsResult.IsSuccess)
            {
                return NotFound();
            }

            var deleteResult = await _clientRepo.DeleteClientAsync(id);
            if (deleteResult.IsSuccess) return NoContent();
            var getResult = await _clientRepo.GetClientByIdAsync(id);
            ModelState.AddModelError("", $"Something went wrong when deleting the record { getResult.Client?.UserName }");
            return StatusCode(500, ModelState);
        }
    }
}
