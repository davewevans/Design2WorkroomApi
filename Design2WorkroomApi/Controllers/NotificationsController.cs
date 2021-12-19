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
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> logger;
        private readonly IMapper mapper;

        public NotificationsController(ILogger<NotificationsController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
