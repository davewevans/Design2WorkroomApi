//using AutoMapper;
//using Design2WorkroomApi.DTOs;
//using Design2WorkroomApi.Helpers;
//using Design2WorkroomApi.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.OData.Query;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Design2WorkroomApi.Controllers
//{
//    [Route("api/reviews")]
//    [ApiController]
//    public class ReviewsController : ControllerBase
//    {
//        private readonly ILogger<ReviewsController> logger;
//        private readonly IMapper mapper;

//        public ReviewsController(ILogger<ReviewsController> logger, IMapper mapper)
//        {
//            this.logger = logger;
//            this.mapper = mapper;
//        }

//        [HttpGet]
//        [EnableQuery]
//        public async Task<IActionResult> GetAllClients()
//        {
//            var result = await _clientRepo.GetAllClientsAsync();

//            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
//            var dtoList = _mapper.Map<List<ClientDto>>(result.Clients);
//            return Ok(dtoList);
//        }

//        [HttpGet("Designer/{designerId:guid}")]
//        public async Task<IActionResult> GetClientsByDesignerId(Guid designerId)
//        {
//            var result = await _clientRepo.GetClientsByDesignerIdAsync(designerId);
//            if (!result.IsSuccess || result.Clients is null) return NotFound(result.ErrorMessage);

//            var clientsDto = new List<ClientDto>();
//            foreach (var client in result.Clients)
//            {
//                var dto = _mapper.Map<ClientDto>(client);
//                var activeStatusResult = await _clientRepo.GetDesignerClientActiveStatusAsync(designerId, client.Id);
//                dto.Active = activeStatusResult.IsActive;
//                dto.Profile.ProfilePicUrl = $"https://avatars.dicebear.com/api/avataaars/{client.Profile.FirstName}.svg";
//                clientsDto.Add(dto);
//            }

//            return Ok(clientsDto);
//        }

//        [HttpGet("{id:guid}", Name = "GetClient")]
//        public async Task<IActionResult> GetClient(Guid id)
//        {
//            var result = await _clientRepo.GetClientByIdAsync(id);

//            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
//            var dto = _mapper.Map<ClientDto>(result.Client);
//            dto.Profile.ProfilePicUrl = $"https://avatars.dicebear.com/api/avataaars/{dto.Profile.FirstName}.svg";
//            return Ok(dto);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateClient([FromBody] ClientCreateDto clientCreateDto)
//        {
//            var existsResult = await _clientRepo.ClientExistsAsync(clientCreateDto.UserName);

//            if (existsResult.Exists)
//            {
//                ModelState.AddModelError("", "Username already exists");
//return StatusCode(403, ModelState);
//}
//            var client = _mapper.Map<ClientModel>(clientCreateDto);
//            client.AppUserRole = _appUserHelper.GetAppUserRole(clientCreateDto.UserRole);
//            client.CreatedAt = DateTime.UtcNow;

//            var createResult = await _clientRepo.CreateClientAsync(client);
//            if (!createResult.IsSuccess)
//            {
//                ModelState.AddModelError("", $"Something went wrong when saving the record {client.UserName}");
//                return StatusCode(500, ModelState);
//            }

//            var dto = _mapper.Map<DesignerDto>(client);
//            return CreatedAtRoute(nameof(GetClient), new { id = dto.Id }, dto);
//        }

//        // PUT: api/Client/5
//        [HttpPut("{id:guid}")]
//        public async Task<IActionResult> UpdateClient(Guid id, ClientUpdateDto clientUpdateDto)
//        {
//            var getResult = await _clientRepo.GetClientByIdAsync(id);

//            if (!getResult.IsSuccess || getResult.Client is null) return NotFound(getResult.ErrorMessage);
//            _mapper.Map(clientUpdateDto, getResult.Client);
//            getResult.Client.UpdatedAt = DateTime.UtcNow;

//            var updateResult = await _clientRepo.UpdateClientAsync(getResult.Client);
//            if (!updateResult.IsSuccess) return NoContent();
//            ModelState.AddModelError("", $"Something went wrong when updating the record { getResult.Client.UserName }");
//            return StatusCode(500, ModelState);
//        }

//        // DELETE: api/Client/5
//        [HttpDelete("{id:guid}")]
//        public async Task<IActionResult> DeleteClient(Guid id)
//        {
//            var existsResult = await _clientRepo.ClientExistsAsync(id);

//            if (!existsResult.IsSuccess)
//            {
//                return NotFound();
//            }

//            var deleteResult = await _clientRepo.DeleteClientAsync(id);
//            if (deleteResult.IsSuccess) return NoContent();
//            var getResult = await _clientRepo.GetClientByIdAsync(id);
//            ModelState.AddModelError("", $"Something went wrong when deleting the record { getResult.Client?.UserName }");
//            return StatusCode(500, ModelState);
//        }
//    }
//}
