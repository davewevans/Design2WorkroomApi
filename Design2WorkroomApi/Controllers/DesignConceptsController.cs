﻿using AutoMapper;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Helpers;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Design2WorkroomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignConceptsController : ControllerBase
    {
        private readonly ILogger<DesignConceptsController> _logger;
        private readonly IMapper _mapper;
        private readonly IDesignConceptRepository _designConceptRepo;
        private readonly IClientRepository _cRepository;
        private readonly IDesignerRepository _dRepository;

        public DesignConceptsController(ILogger<DesignConceptsController> logger, IMapper mapper, IDesignConceptRepository designConceptRepo, IClientRepository cRepository, IDesignerRepository dRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _designConceptRepo = designConceptRepo;
            _cRepository = cRepository;
            _dRepository = dRepository;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _designConceptRepo.GetAllDesignConceptsAsync();

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dtoList = _mapper.Map<List<DesignConceptDto>>(result.DesignConcepts);

            foreach(var item in dtoList)
            {
                var client = await _cRepository.GetClientByIdAsync(item.ClientId);
                item.ClientDetails = _mapper.Map<ClientDto>(client.Client);

                var designer = await _dRepository.GetDesignerByIdAsync(item.DesignerId);
                item.DesignerDetails = _mapper.Map<DesignerDto>(designer.Designer);
            }

            return Ok(dtoList);
        }

        [HttpGet("Designer/{designerId:guid}")]
        public async Task<IActionResult> GetDesignConceptsByDesignerId(Guid designerId)
        {
            var result = await _designConceptRepo
                .GetDesignConceptsByConditionAsync(x => x.DesignerId == designerId);
            if (!result.IsSuccess || result.DesignConcepts is null) return NotFound(result.ErrorMessage);
            
            var designConceptsDto = _mapper.Map<List<DesignConceptDto>>(result.DesignConcepts);

            return Ok(designConceptsDto);
        }

        [HttpGet("Designer/{designerId:guid}/Client/{clientId:guid}")]
        public async Task<IActionResult> GetDesignConceptsByDesignerAndClientIds(Guid designerId, Guid clientId)
        {
            var result = await _designConceptRepo
                .GetDesignConceptsByConditionAsync(x => x.DesignerId == designerId && x.ClientId == clientId);
            if (!result.IsSuccess || result.DesignConcepts is null) return NotFound(result.ErrorMessage);

            var designConceptsDto = _mapper.Map<List<DesignConceptDto>>(result.DesignConcepts);

            return Ok(designConceptsDto);
        }

        [HttpGet("{id:guid}", Name = "GetDesignConcept")]
        public async Task<IActionResult> GetDesignConcept(Guid id)
        {
            var result = await _designConceptRepo.GetDesignConceptByIdAsync(id);

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dto = _mapper.Map<DesignConceptDto>(result.DesignConcept);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDesignConcept([FromBody] DesignConceptCreateDto designConceptCreateDto)
        {
            var designConcept = _mapper.Map<DesignConceptModel>(designConceptCreateDto);

            designConcept.CreatedAt = DateTime.UtcNow;

            var createResult = await _designConceptRepo.CreateDesignConceptAsync(designConcept);
            if (!createResult.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record.");
                return StatusCode(500, ModelState);
            }

            var dto = _mapper.Map<DesignConceptDto>(designConcept);
            return CreatedAtRoute(nameof(GetDesignConcept), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateDesignConcept(Guid id, DesignConceptUpdateDto designConceptUpdateDto)
        {
            var getResult = await _designConceptRepo.GetDesignConceptByIdAsync(id);

            if (!getResult.IsSuccess || getResult.DesignConcept is null) return NotFound(getResult.ErrorMessage);
            _mapper.Map(designConceptUpdateDto, getResult.DesignConcept);
            //getResult.DesignConcept.IsApproved = designConceptUpdateDto.IsApproved;
            getResult.DesignConcept.UpdatedAt = DateTime.UtcNow;

            var updateResult = await _designConceptRepo.UpdateDesignConceptsAsync(getResult.DesignConcept);
            if (!updateResult.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when updating the record");
            return StatusCode(500, ModelState);

        }

        // DELETE: api/Client/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var existsResult = await _designConceptRepo.DesignConceptExistsAsync(id);

            if (!existsResult.IsSuccess)
            {
                return NotFound();
            }

            var deleteResult = await _designConceptRepo.DeleteDesignConceptAsync(id);
            if (deleteResult.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when deleting the record.");
            return StatusCode(500, ModelState);
        }

        [HttpPost("CreateDesignConceptsApproval")]
        public async Task<IActionResult> CreateDesignConceptsApproval([FromBody] DesignConceptsApprovalsCreateDto designConceptsApprovalsCreateDto)
        {
            var designConceptsApproval = _mapper.Map<DesignConceptsApprovalModel>(designConceptsApprovalsCreateDto);

            designConceptsApproval.CreatedAt = DateTime.UtcNow;

            var createResult = await _designConceptRepo.CreateDesignConceptsApprovalAsync(designConceptsApproval);
            if (!createResult.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the create designconcepts approval data.");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
