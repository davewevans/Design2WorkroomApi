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
    [Route("api/interiordecorations")]
    [ApiController]
    public class InteriorDecorationsController : ControllerBase
    {
        private readonly ILogger<InteriorDecorationsController> logger;
        private readonly IMapper mapper;

        public InteriorDecorationsController(ILogger<InteriorDecorationsController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
