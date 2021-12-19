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
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ILogger<ReviewsController> logger;
        private readonly IMapper mapper;

        public ReviewsController(ILogger<ReviewsController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
