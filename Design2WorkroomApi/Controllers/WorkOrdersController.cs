using AutoMapper;
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

        public WorkOrdersController(ILogger<WorkOrdersController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
