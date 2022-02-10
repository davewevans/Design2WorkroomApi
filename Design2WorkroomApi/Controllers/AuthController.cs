using AutoMapper;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Enums;
using Design2WorkroomApi.Helpers;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Design2WorkroomApi.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly AppUserHelper _appUserHelper;
        private readonly IDesignerRepository _designerRepo;
        private readonly IAppRolesProvider _appRolesProvider;
        private const string AppRolesAttributeName = "extension_AppRoles";
        private const string AppUserIdAttributeName = "extension_UserId";
        private const string AppUserName = "displayName";
        private static Random _randomizer = new Random();
        private readonly IInvitationRepository _invitationRepository;

        public AuthController(ILogger<AuthController> logger,
            IMapper mapper,
            AppUserHelper appUserHelper,
            IDesignerRepository designerRepo,
            IAppRolesProvider appRolesProvider,
            IInvitationRepository invitationRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _appUserHelper = appUserHelper;
            _appRolesProvider = appRolesProvider;
            _designerRepo = designerRepo;
            _invitationRepository = invitationRepository;
        }

        [HttpPost(nameof(CreateNewUser))]
        public async Task<IActionResult> CreateNewUser([FromBody] JsonElement body)
        {
            _logger.LogInformation("CreateNewUser being requested.");

            // Log the incoming request body.
            _logger.LogInformation("Request body:");
            _logger.LogInformation(JsonSerializer.Serialize(body, new JsonSerializerOptions { WriteIndented = true }));
            //return GetValidationErrorApiResponse("CreateUser-InternalError", body.ToString());
            try
            {
                // Get the object id of the user that is signing in.
                var objectId = body.GetProperty("objectId").GetString();
                //var appUserRole = body.GetProperty("AppRoles").GetString();
                
                var email = body.GetProperty("email").GetString();
                var firstName = body.GetProperty("givenName").GetString();
                var lastName = body.GetProperty("surname").GetString();
                var postalCode = body.GetProperty("postalCode").GetString();
                var city = body.GetProperty("city").GetString();
                var state = body.GetProperty("state").GetString();
                var country = body.GetProperty("country").GetString();
                return GetContinueApiResponse("GetAppRoles-Succeeded", "Your app roles were successfully determined.", "", "", firstName + " " + lastName);
                //var designer = new DesignerModel(email, "")
                //{
                //    AppUserRole = AppUserRole.Designer,
                //    CreatedAt = DateTime.UtcNow,
                //    Profile = new ProfileModel(email, firstName, lastName, null,null, postalCode,null,null,null,null,city,state)
                //};

                //await _designerRepo.CreateDesignerAsync(designer);
                
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return GetValidationErrorApiResponse("CreateUser-InternalError", "An error occurred while creating user, please try again later.");
            }

            return Ok();
        }

        [HttpPost(nameof(UpdateUserWithInvitationCode))]
        public async Task<IActionResult> UpdateUserWithInvitationCode(InvitationModel data)
        {
            try
            {
                await _invitationRepository.UpdateUserWithInvitationAsync(data);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing request body: " + ex.ToString());
                return GetBlockPageApiResponse("GetAppRoles-InternalError", "An error occurred, please try again later.");
            }
        }

        [HttpPost(nameof(CreateUserWithInvitationCode))]
        public async Task<IActionResult> CreateUserWithInvitationCode(InvitationModel data)
        {
            try
            {
                data.InvitationCode = GenerateInvitationCode();
                var response = await _invitationRepository.CreateUserWithInvitationAsync(data);

                Guid Id = response.InvitationId;

                return Ok(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing request body: " + ex.ToString());
                return GetBlockPageApiResponse("GetAppRoles-InternalError", "An error occurred while determining your app roles, please try again later.");
            }
        }

        [HttpGet("{id:guid}", Name = "GetInvitationFromId")]
        public async Task<IActionResult> GetInvitationFromId(Guid id)
        {
            try
            {
                InvitationModel? modal = (await _invitationRepository.GetInvitationByIdAsync(id)).Invitation;

                return Ok(modal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing request body: " + ex.ToString());
                return GetBlockPageApiResponse("GetAppRoles-InternalError", "An error occurred while determining your app roles, please try again later.");
            }
        }

        [HttpPost(nameof(GetAppRoles))]
        public async Task<IActionResult> GetAppRoles([FromBody] JsonElement body)
        {
            // Azure AD B2C calls into this API when a user is attempting to sign in.
            // We expect a JSON object in the HTTP request which contains the input claims.
            try
            {
                _logger.LogInformation("App roles are being requested.");
                _logger.LogTrace(JsonSerializer.Serialize(body, new JsonSerializerOptions { WriteIndented = true }));

                // Log the incoming request body.
                _logger.LogInformation("Request body:");
                _logger.LogInformation(JsonSerializer.Serialize(body, new JsonSerializerOptions { WriteIndented = true }));
                
                // Get the object id of the user that is signing in.
                var objectId = body.GetProperty("objectId").GetString();

                // Get the client id of the app that the user is signing in to.
                var clientId = body.GetProperty("client_id").GetString();
                var email = body.GetProperty("email").GetString();
                var firstName = "";
                if (body.TryGetProperty("givenName", out JsonElement jsonElement5))
                {
                    firstName = body.GetProperty("givenName").GetString();
                }
                var lastName = "";
                if (body.TryGetProperty("surname", out JsonElement jsonElement4))
                {
                    lastName = body.GetProperty("surname").GetString();
                }
                //var postalCode = body.GetProperty("postalCode").GetString();
                var city = "";
                if (body.TryGetProperty("city",out JsonElement jsonElement3))
                {
                    city = body.GetProperty("city").GetString();
                }

                var state = "";
                if (body.TryGetProperty("state", out JsonElement jsonElement1))
                {
                    state = body.GetProperty("state").GetString();
                }
                var country = "";
                if (body.TryGetProperty("country", out JsonElement jsonElement2))
                {
                    country = body.GetProperty("country").GetString();
                }
                var existsResult = await _designerRepo.DesignerExistsAsync(email);
                //if(existsResult.Exists)
                //{
                //    var getExistsUSer = await _designerRepo.GetDesignerByEmailAsync(email);
                //    if(getExistsUSer.Designer != null)
                //    {
                //        getExistsUSer.Designer.B2CObjectId = objectId;
                //    }
                   
                //}
                if (!existsResult.Exists)
                {
                    var designer = new DesignerModel(email, objectId)
                    {
                        AppUserRole = AppUserRole.Designer,
                        CreatedAt = DateTime.UtcNow,
                        Profile = new ProfileModel(email, firstName, lastName, null, null, null, null, null, null, null, city, state, country)
                        {
                            CreatedAt = DateTime.UtcNow
                        }
                    };

                    var createResult = await _designerRepo.CreateDesignerAsync(designer);
                    var appRoles_Value = AppUserRole.Designer.ToString();// (appRoles == null || !appRoles.Any()) ? null : string.Join(' ', appRoles);

                    return GetContinueApiResponse("GetAppRoles-Succeeded", "Your app roles were successfully determined.", appRoles_Value, createResult.UserId, firstName + " " + lastName);
                    //return GetValidationErrorApiResponse("GetAppRoles-InternalError", "Something went wrong...." + body.ToString());
                }

                return GetBlockPageApiResponse("GetAppRoles-InternalError", "$$$$ User Exist Response : " + body);

                // Retrieve the app roles assigned to the user for the requested application.
                var appRoles = await _appRolesProvider.GetAppRolesAsync(email, objectId);
                if(appRoles.IsSuccess)
                {
                    //return GetValidationErrorApiResponse("GetAppRoles-InternalError", "Something went wrong...." + body.ToString() + "$$appRoles : " + appRoles.AppUserRole + "$$UserId : " + appRoles.UserId + "Name : " + firstName + " " + lastName);
                    return GetContinueApiResponse("GetAppRoles-Succeeded", "Your app roles were successfully determined.", appRoles.AppUserRole, appRoles.UserId, firstName + " " + lastName);
                }
                else
                {
                    return GetBlockPageApiResponse("GetAppRoles-InternalError", appRoles.ErrorMessage + "$$$$ Response : " + body);
                }
                // Custom user attributes in Azure AD B2C cannot be collections, so we emit them
                // into a single claim value separated with spaces.
                //var appRolesValue = AppUserRole.Designer.ToString();// (appRoles == null || !appRoles.Any()) ? null : string.Join(' ', appRoles);

                //return GetBlockPageApiResponse("GetAppRoles-InternalError", "An error occurred while determining your app roles, please try again later.");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Error while processing request body: " + exc.ToString());
                return GetBlockPageApiResponse("GetAppRoles-InternalError", exc.ToString());
            }
        }       

        private IActionResult GetContinueApiResponse(string code, string userMessage, string appRoles, string UserId = "", string userName = "")
        {
            return GetB2cApiConnectorResponse("Continue", code, userMessage, 200, appRoles, UserId, userName);
        }

        private IActionResult GetValidationErrorApiResponse(string code, string userMessage)
        {
            return GetB2cApiConnectorResponse("ValidationError", code, userMessage, 400, null);
        }

        private IActionResult GetBlockPageApiResponse(string code, string userMessage)
        {
            return GetB2cApiConnectorResponse("ShowBlockPage", code, userMessage, 400, null);
        }

        private IActionResult GetB2cApiConnectorResponse(string action, string code, string userMessage, int statusCode, string appRoles, string UserId = "", string userName = "")
        {
            var responseProperties = new Dictionary<string, object>
            {
                { "version", "1.0.0" },
                { "action", action },
                { "userMessage", userMessage },
                { AppRolesAttributeName, appRoles },
                { AppUserIdAttributeName,UserId },
                { AppUserName, userName }
            };
            if (statusCode != 200)
            {
                // Include the status in the body as well, but only for validation errors.
                responseProperties["status"] = statusCode.ToString();
            }
            return new JsonResult(responseProperties) { StatusCode = statusCode };
        }

        private string GenerateInvitationCode()
        {
            char[] characters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int charLength = 6;
            var sb = new StringBuilder();

            for (int i = 0; i < charLength; i++)
            {
                char randomChar = characters[_randomizer.Next(characters.Length)];
                sb.Append(randomChar);
            }

            return sb.ToString();
        }
    }
}