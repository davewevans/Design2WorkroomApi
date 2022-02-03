using AutoMapper;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Controllers
{
    [Route("api/workorders")]
    [ApiController]
    public class WorkOrdersController : ControllerBase
    {
        private readonly ILogger<WorkOrdersController> logger;
        private readonly IMapper mapper;
        private readonly IWorkOrdersRepository _workOrders;

        public WorkOrdersController(ILogger<WorkOrdersController> logger, IMapper mapper, IWorkOrdersRepository workOrders)
        {
            this.logger = logger;
            this.mapper = mapper;
            this._workOrders = workOrders;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkOrder([FromBody] WorkOrderCreateDto WorkOrder)
        {
            var data = mapper.Map<WorkOrderModel>(WorkOrder);
            data.CreatedAt = DateTime.UtcNow;

            var createResult = await _workOrders.CreateWorkOrderAsync(data);
            if (!createResult.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {WorkOrder.WorkOrderNumber}");
                return StatusCode(500, ModelState);
            }

            return Ok(createResult.Id);
        }

        [HttpGet("{id:guid}", Name = "GetWorkOrder")]
        public async Task<IActionResult> GetWorkOrder(Guid id)
        {
            var result = await _workOrders.GetWorkOrderByIdAsync(id);

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dto = mapper.Map<WorkOrderDto>(result.WorkOrder);
            return Ok(dto);
        }

        [HttpGet("GetAllWorkOrders")]
        public async Task<IActionResult> GetAllWorkOrders()
        {
            var result = await _workOrders.GetAllWorkOrdersAsync();

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dto = mapper.Map<List<WorkOrderDto>>(result.WorkOrders);
            return Ok(dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWorkOrder(Guid id, WorkOrderDto WorkOrder)
        {
            var getResult = await _workOrders.GetWorkOrderByIdAsync(id);

            if (!getResult.IsSuccess || getResult.WorkOrder is null) return NotFound(getResult.ErrorMessage);
            mapper.Map(WorkOrder, getResult.WorkOrder);
            getResult.WorkOrder.UpdatedAt = DateTime.UtcNow;

            var updateResult = await _workOrders.UpdateWorkOrderAsync(getResult.WorkOrder);
            if (!updateResult.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record { getResult.WorkOrder.WorkOrderNumber }");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWorkOrderAsync(Guid id)
        {
            var existsResult = await _workOrders.GetWorkOrderByIdAsync(id);

            if (!existsResult.IsSuccess)
            {
                return NotFound();
            }

            var deleteResult = await _workOrders.DeleteWorkOrderAsync(id);
            if (deleteResult.IsSuccess) return NoContent();
            var getResult = await _workOrders.GetWorkOrderByIdAsync(id);
            ModelState.AddModelError("", $"Something went wrong when deleting the record { getResult.WorkOrder?.WorkOrderNumber }");
            return StatusCode(500, ModelState);
        }
    }
}