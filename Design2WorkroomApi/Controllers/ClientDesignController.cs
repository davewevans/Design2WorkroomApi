using AutoMapper;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Design2WorkroomApi.Controllers
{
    [Route("api/ClientDesign")]
    [ApiController]
    public class ClientDesignController : Controller
    {
        private readonly ILogger<DesignersController> _logger;
        private readonly IMapper _mapper;
        private readonly IClientDesignRepository _clientDesignRepository;

        public ClientDesignController(ILogger<DesignersController> logger,
            IMapper mapper,
            IClientDesignRepository clientDesignRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _clientDesignRepository = clientDesignRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClientDesign([FromBody] ClientDesignDto clientDesignDto)
        {
            try
            {
                ClientDesignModel design = _mapper.Map<ClientDesignModel>(clientDesignDto);
                design.CreatedAt = DateTime.UtcNow;

                var createResult = await _clientDesignRepository.CreateClientDesignAsync(design);
                if (!createResult.IsSuccess)
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record");
                    return StatusCode(500, ModelState);
                }

                var dto = _mapper.Map<ClientDesignDto>(design);
                return CreatedAtRoute(nameof(GetClientDesign), new { id = dto.Id }, dto);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id:guid}", Name = "GetClientDesign")]
        public async Task<IActionResult> GetClientDesign(Guid id)
        {
            var result = await _clientDesignRepository.GetClientDesignByIdAsync(id);

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dto = _mapper.Map<ClientDesignDto>(result.ClientDesign);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientDesigns()
        {
            var result = await _clientDesignRepository.GetAllClientDesignsAsync();

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dtoList = _mapper.Map<List<ClientDesignDto>>(result.ClientDesigns);
            return Ok(dtoList);
        }
    }
}