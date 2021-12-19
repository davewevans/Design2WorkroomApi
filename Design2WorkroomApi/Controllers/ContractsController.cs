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
    [Route("api/contracts")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly ILogger<ContractsController> logger;
        private readonly IMapper mapper;

        public ContractsController(ILogger<ContractsController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
