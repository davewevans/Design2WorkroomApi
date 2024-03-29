﻿using AutoMapper;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Enums;
using Design2WorkroomApi.Helpers;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinifyAPI;

namespace Design2WorkroomApi.Controllers
{
    // TODO
    // versioning
    // https://www.udemy.com/course/quick-introduction-to-aspnet-mvc-core-20/learn/lecture/18079125#overview
    // https://levelup.gitconnected.com/net-5-use-api-versioning-4ffe8b0210b2


    [Route("api/workrooms")]
    [ApiController]
    public class WorkroomsController : ControllerBase
    {
        private readonly ILogger<WorkroomsController> _logger;
        private readonly IMapper _mapper;
        private readonly IWorkroomRepository _workroomRepo;
        private readonly IUserRepository _userRepository;
        private readonly AppUserHelper _appUserHelper;

        public WorkroomsController(ILogger<WorkroomsController> logger, 
            IMapper mapper,
            IWorkroomRepository workroomRepo,
            AppUserHelper appUserHelper,
            IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _workroomRepo = workroomRepo;
            _appUserHelper = appUserHelper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkrooms()
        {
            var result = await _workroomRepo.GetAllWorkroomsAsync();

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dtoList = _mapper.Map<List<WorkroomDto>>(result.Workrooms);
            return Ok(dtoList);
        }

        [HttpGet("{id:Guid}", Name = "GetWorkroom")]
        public async Task<IActionResult> GetWorkroom(Guid id)
        {
            var result = await _workroomRepo.GetWorkroomByIdAsync(id);

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dto = _mapper.Map<WorkroomDto>(result.Workroom);
            return Ok(dto);
        }


        [HttpGet("WorkroomsByDesignerId/{designerId}")]
        public async Task<IActionResult> GetWorkroomsByDesignerId(Guid designerId)
        {
            var result = await _workroomRepo.GetWorkroomsByDesignerIdAsync(designerId);
            if (!result.IsSuccess || result.Workrooms is null) return NotFound(result.ErrorMessage);

            var workroomsDto = new List<WorkroomDto>();
            foreach (var workroom in result.Workrooms)
            {
                var dto = _mapper.Map<WorkroomDto>(workroom);
                var getStatusResult = await _workroomRepo.GetDesignerWorkroomActiveStatusAsync(designerId, workroom.Id);
                dto.Active = getStatusResult.IsActive;
                dto.Profile.ProfilePicUrl = $"https://avatars.dicebear.com/api/avataaars/{workroom.Profile.WorkroomName}.svg";
                workroomsDto.Add(dto);
            }

            return Ok(workroomsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkroom([FromBody] WorkroomCreateDto workroomCreateDto)
        {
            var existsResult = await _workroomRepo.WorkroomExistsAsync(workroomCreateDto.UserName);

            if (existsResult.Exists)
            {
                ModelState.AddModelError("", "Username already exists");
                return StatusCode(403, ModelState);
            }

            WorkroomModel workroom = _mapper.Map<WorkroomModel>(workroomCreateDto);
            workroom.AppUserRole = _appUserHelper.GetAppUserRole(workroomCreateDto.UserRole);
            workroom.CreatedAt = DateTime.UtcNow;

            //
            /// client.InvitationAccepted
            /// 

            var createResult = await _workroomRepo.CreateWorkroomReturnWorkroomAsync(workroom);
            if (!createResult.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {workroom.UserName}");
                return StatusCode(500, ModelState);
            }

            AppUserAppUser appUserAppUser = new AppUserAppUser();
            appUserAppUser.Active = true;
            appUserAppUser.AppUserParentId = workroomCreateDto.AppUserAppUser.AppUserParentId;
            appUserAppUser.AppUserChildId = createResult.Workroom.Id;
            var result = await _userRepository.CreateAppUserAppUserAsync(appUserAppUser);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record CreateAppUserAppUserAsync");
                return StatusCode(500, ModelState);
            }

            //old code
            var dto = _mapper.Map<WorkroomDto>(workroom);

            return CreatedAtRoute(nameof(GetWorkroom), new { id = dto.Id }, dto);
        }

        // PUT: api/Workroom/5
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateWorkroom(Guid id, WorkroomUpdateDto workroomUpdateDto)
        {
            var getResult = await _workroomRepo.GetWorkroomByIdAsync(id);

            if (!getResult.IsSuccess || getResult.Workroom is null) return NotFound(getResult.ErrorMessage);
            _mapper.Map(workroomUpdateDto, getResult.Workroom);
            getResult.Workroom.UpdatedAt = DateTime.UtcNow;

            //
            /// client.InvitationAccepted
            /// 

            var updateResult = await _workroomRepo.UpdateWorkroomAsync(getResult.Workroom);
            if (!updateResult.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when updating the record { getResult.Workroom.UserName }");
            return StatusCode(500, ModelState);
        }

        // DELETE: api/Workroom/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteWorkroom(Guid id)
        {
            var existsResult = await _workroomRepo.WorkroomExistsAsync(id);

            if (!existsResult.IsSuccess)
            {
                return NotFound();
            }

            var deleteResult = await _workroomRepo.DeleteWorkroomAsync(id);
            if (deleteResult.IsSuccess) return NoContent();
            var getResult = await _workroomRepo.GetWorkroomByIdAsync(id);
            ModelState.AddModelError("", $"Something went wrong when deleting the record { getResult.Workroom?.UserName }");
            return StatusCode(500, ModelState);
        }
    }
}
