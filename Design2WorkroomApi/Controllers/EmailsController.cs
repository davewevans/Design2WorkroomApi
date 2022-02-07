using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Design2WorkroomApi.Repository.Contracts;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Helpers;
using Design2WorkroomApi.Models;
using Microsoft.AspNetCore.OData.Query;
using PostmarkDotNet.Webhooks;

namespace Design2WorkroomApi.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly ILogger<EmailsController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmailRepository _emailRepo;
        private readonly IPostmarkEmailSender _emailSender;

        public EmailsController(ILogger<EmailsController> logger, IMapper mapper, IEmailRepository emailRepository, IPostmarkEmailSender emailSender)
        {
            _logger = logger;
            _mapper = mapper;
            _emailRepo = emailRepository;
            _emailSender = emailSender;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllEmails()
        {
            var result = await _emailRepo.GetAllEmailsAsync();

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dtoList = _mapper.Map<List<EmailDto>>(result.Emails);
            return Ok(dtoList);
        }

        [HttpGet("Designer/{designerId:guid}")]
        public async Task<IActionResult> GetEmailsByDesignerId(Guid designerId)
        {
            var result = await _emailRepo.GetEmailsByConditionAsync(x => x.DesignerId == designerId);
            if (!result.IsSuccess) return NotFound(result.ErrorMessage);

            var emailsDto = _mapper.Map<List<EmailDto>>(result.Emails);

            return Ok(emailsDto);
        }

        [HttpGet("Client/{clientId:guid}")]
        public async Task<IActionResult> GetEmailsByClientId(Guid clientId)
        {
            var result = await _emailRepo.GetEmailsByConditionAsync(x => x.ClientId == clientId);
            if (!result.IsSuccess) return NotFound(result.ErrorMessage);

            var emailsDto = _mapper.Map<List<EmailDto>>(result.Emails);

            return Ok(emailsDto);
        }

        [HttpGet("Workroom/{workroomId:guid}")]
        public async Task<IActionResult> GetEmailsByWorkroomId(Guid workroomId)
        {
            var result = await _emailRepo.GetEmailsByConditionAsync(x => x.WorkroomId == workroomId);
            if (!result.IsSuccess) return NotFound(result.ErrorMessage);

            var emailsDto = _mapper.Map<List<EmailDto>>(result.Emails);

            return Ok(emailsDto);
        }

        [HttpGet("{id:guid}", Name = "GetEmail")]
        public async Task<IActionResult> GetEmail(Guid id)
        {
            var result = await _emailRepo.GetEmailByIdAsync(id);

            if (!result.IsSuccess) return NotFound(result.ErrorMessage);
            var dto = _mapper.Map<EmailDto>(result.Email);
            return Ok(dto);
        }

        [HttpPost("PostInboundWebhook")]
        public async Task<IActionResult> PostInboundWebhook([FromBody] PostmarkInboundWebhookMessage message)
        {
            var createResult = await _emailRepo.InboundEmailPostmarkWebHookAsync(message);
            if (!createResult.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record.");
                _logger.LogError(createResult.ErrorMessage);
                return BadRequest(createResult.ErrorMessage);
                //return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmail([FromBody] EmailCreateDto emailCreateDto)
        {
            var email = _mapper.Map<EmailModel>(emailCreateDto);
            email.CreatedAt = DateTime.UtcNow;

            var createResult = await _emailRepo.CreateEmailAsync(email);
            if (!createResult.IsSuccess)
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record.");
                return StatusCode(500, ModelState);
            }
            var dto = _mapper.Map<EmailDto>(email);
            return CreatedAtRoute(nameof(GetEmail), new { id = dto.Id }, dto);
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailCreateDto emailCreateDto)
        {
            var email = new PostmarkEmailModel
            {
                ToEmailAddress = emailCreateDto.ToEmailAddress,
                FromEmailAddress = emailCreateDto.FromEmailAddress,
                Subject = emailCreateDto.Subject,
                TextBody = emailCreateDto.TextBody,
                HtmlBody = emailCreateDto.HtmlBody,
                Tag = emailCreateDto.Tag,
                Headers = null // new MailHeader("X-D2W", value: "Test Header content"),
            };

            try
            {
                await _emailSender.SendEmailPostmarkAsync(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        // PUT: api/Email/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmail(Guid id, EmailUpdateDto emailUpdateDto)
        {
            var getResult = await _emailRepo.GetEmailByIdAsync(id);

            if (!getResult.IsSuccess || getResult.Email is null) return NotFound(getResult.ErrorMessage);
            _mapper.Map(emailUpdateDto, getResult.Email);
            getResult.Email.UpdatedAt = DateTime.UtcNow;

            var updateResult = await _emailRepo.UpdateEmailAsync(getResult.Email);
            if (!updateResult.IsSuccess) return NoContent();
            ModelState.AddModelError("", $"Something went wrong when updating the record.");
            return StatusCode(500, ModelState);
        }

        // DELETE: api/Email/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmail(Guid id)
        {
            var existsResult = await _emailRepo.EmailExistsAsync(id);

            if (!existsResult.IsSuccess)
            {
                return NotFound();
            }

            var deleteResult = await _emailRepo.DeleteEmailAsync(id);
            if (deleteResult.IsSuccess) return NoContent();
            var getResult = await _emailRepo.GetEmailByIdAsync(id);
            ModelState.AddModelError("", $"Something went wrong when deleting the record.");
            return StatusCode(500, ModelState);
        }


    }
}
