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

            return CreatedAtRoute(nameof(GetWorkOrder), new { id = data.Id }, data);
        }

        [HttpGet("{id:guid}", Name = "GetWorkOrder")]
        public async Task<IActionResult> GetWorkOrder(Guid id)
        {
            var result = await _workOrders.GetWorkOrderByIdAsync(id);

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dto = mapper.Map<WorkOrderDto>(result.Client);
            return Ok(dto);
        }
    }
}