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
    [Route("api/subscriptionplans")]
    [ApiController]
    public class SubscriptionPlansController : ControllerBase
    {
        private readonly ILogger<SubscriptionPlansController> logger;
        private readonly IMapper mapper;

        public SubscriptionPlansController(ILogger<SubscriptionPlansController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
