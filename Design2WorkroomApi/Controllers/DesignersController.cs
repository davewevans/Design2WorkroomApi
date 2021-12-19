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
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using TinifyAPI;

namespace Design2WorkroomApi.Controllers
{
    // TODO
    // Versioning
    // https://www.udemy.com/course/quick-introduction-to-aspnet-mvc-core-20/learn/lecture/18079125#overview
    // https://levelup.gitconnected.com/net-5-use-api-versioning-4ffe8b0210b2

    [Route("api/designers")]
    [ApiController]
    public class DesignersController : ControllerBase
    {
        private readonly ILogger<DesignersController> _logger;
        private readonly IMapper _mapper;
        private readonly IDesignerRepository _designerRepo;
        private readonly AppUserHelper _appUserHelper;

        public DesignersController(ILogger<DesignersController> logger, 
            IMapper mapper,
            IDesignerRepository designerRepo,
            AppUserHelper appUserHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _designerRepo = designerRepo;
            _appUserHelper = appUserHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDesigners()
        {
            var result = await _designerRepo.GetAllDesignersAsync();

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dtoList = _mapper.Map<List<DesignerDto>>(result.Designers);
            return Ok(dtoList);
        }

        [HttpGet("{id:guid}", Name = "GetDesigner")]
        public async Task<IActionResult> GetDesigner(Guid id)
        {
            var result = await _designerRepo.GetDesignerByIdAsync(id);

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dto = _mapper.Map<DesignerDto>(result.Designer);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDesigner([FromBody] DesignerCreateDto designerCreateDto)
        {
            var existsResult = await _designerRepo.DesignerExistsAsync(designerCreateDto.UserName);

            if (existsResult.Exists)
            {
                ModelState.AddModelError("", "Username already exists");
                return StatusCode(403, ModelState);
            }
            var designer = _mapper.Map<DesignerModel>(designerCreateDto);
            designer.AppUserRole = _appUserHelper.GetAppUserRole(designerCreateDto.UserRole);
            designer.CreatedAt = DateTime.UtcNow;

            var createResult = await _designerRepo.CreateDesignerAsync(designer);
            if (!createResult.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {designer.UserName}");
                return StatusCode(500, ModelState);
            }

            var dto = _mapper.Map<DesignerDto>(designer);
            return CreatedAtRoute(nameof(GetDesigner), new { id = dto.Id }, dto);
        }

        // PUT: api/Designer/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateDesigner(Guid id, DesignerUpdateDto designerUpdateDto)
        {
            var getResult = await _designerRepo.GetDesignerByIdAsync(id);

            if (!getResult.IsSuccess || getResult.Designer is null) return NotFound(getResult.ErrorMessage);
            _mapper.Map(designerUpdateDto, getResult.Designer);
            getResult.Designer.UpdatedAt = DateTime.UtcNow;

            var updateResult = await _designerRepo.UpdateDesignerAsync(getResult.Designer);
            if (!updateResult.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when updating the record { getResult.Designer.UserName }");
            return StatusCode(500, ModelState);

        }


        // PUT /designers/5/clients/10
        [HttpPut("{designerId}/clients/{clientId}")]
        public async Task<IActionResult> AddClientToDesigner(Guid designerId, Guid clientId)
        {
            var result = await _designerRepo.AddClientToDesignerAsync(designerId, clientId);
            if (result.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when updating the record");
            return StatusCode(500, ModelState);

        }

        // PUT /designers/5/workrooms/10
        [HttpPut("{designerId}/workrooms/{workroomId}")]
        public async Task<IActionResult> AddWorkroomToDesigner(Guid designerId, Guid workroomId)
{
            var result = await _designerRepo.AddWorkroomToDesignerAsync(designerId, workroomId);
            if (result.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when updating the record");
            return StatusCode(500, ModelState);
}


        // DELETE: /designers/5/clients/10
        [HttpDelete("{designerId}/clients/{clientId}")]
        public async Task<IActionResult> DeleteDesignerClient(Guid designerId, Guid clientId)
        {
            var result = await _designerRepo.RemoveDesignerClientAsync(designerId, clientId);
            if (result.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when deleting the record");
            return StatusCode(500, ModelState);
        }

        // DELETE: /designers/5/workrooms/10
        [HttpDelete("{designerId}/workrooms/{workroomId}")]
        public async Task<IActionResult> DeleteDesignerWorkroom(Guid designerId, Guid workroomId)
        {
            var result = await _designerRepo.RemoveDesignerWorkroomAsync(designerId, workroomId);
            if (result.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when deleting the record");
            return StatusCode(500, ModelState);
        }


        // DELETE: api/designers/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDesigner(Guid id)
        {
            var existsResult = await _designerRepo.DesignerExistsAsync(id);

            if (!existsResult.IsSuccess)
            {
                return NotFound();
            }

            var deleteResult = await _designerRepo.DeleteDesignerAsync(id);
            if (deleteResult.IsSuccess) return NoContent();
            var getResult = await _designerRepo.GetDesignerByIdAsync(id);
            ModelState.AddModelError("", $"Something went wrong when deleting the record { getResult.Designer?.UserName }");
            return StatusCode(500, ModelState);
        }
    }
}
