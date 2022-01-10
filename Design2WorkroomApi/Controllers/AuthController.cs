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
        private static Random _randomizer = new Random();

        public AuthController(ILogger<AuthController> logger,
            IMapper mapper,
            AppUserHelper appUserHelper,
            IDesignerRepository designerRepo,
            IAppRolesProvider appRolesProvider)
        {
            _logger = logger;
            _mapper = mapper;
            _appUserHelper = appUserHelper;
            _appRolesProvider = appRolesProvider;
            _designerRepo = designerRepo;
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
                var designer = new DesignerModel(email, "")
                {
                    AppUserRole = AppUserRole.Designer,
                    CreatedAt = DateTime.UtcNow,
                    Profile = new ProfileModel(email, firstName, lastName, null,null, postalCode,null,null,null,null,city,state)
                };

                await _designerRepo.CreateDesignerAsync(designer);
                
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                _logger.LogInformation(ex.Message);
                return GetValidationErrorApiResponse("CreateUser-InternalError", "An error occurred while creating user, please try again later.");
            }

            return Ok();
        }

        [HttpPost(nameof(CreateUserWithInvitationCode))]
        public IActionResult CreateUserWithInvitationCode()
        {



            return Ok();
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
                var firstName = body.GetProperty("givenName").GetString();
                var lastName = body.GetProperty("surname").GetString();
                //var postalCode = body.GetProperty("postalCode").GetString();
                var city = body.GetProperty("city").GetString();
                var state = body.GetProperty("state").GetString();
                var country = body.GetProperty("country").GetString();
                var existsResult = await _designerRepo.DesignerExistsAsync(email);
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

                    return GetContinueApiResponse("GetAppRoles-Succeeded", "Your app roles were successfully determined.", appRoles_Value, createResult.UserId);
                }
                

                // Retrieve the app roles assigned to the user for the requested application.
                var appRoles = await _appRolesProvider.GetAppRolesAsync(email, objectId);
                if(appRoles.IsSuccess)
                {
                    return GetContinueApiResponse("GetAppRoles-Succeeded", "Your app roles were successfully determined.", appRoles.AppUserRole);
                }
                else
                {
                    return GetBlockPageApiResponse("GetAppRoles-InternalError", appRoles.ErrorMessage);
                }
                // Custom user attributes in Azure AD B2C cannot be collections, so we emit them
                // into a single claim value separated with spaces.
                //var appRolesValue = AppUserRole.Designer.ToString();// (appRoles == null || !appRoles.Any()) ? null : string.Join(' ', appRoles);

                //return GetBlockPageApiResponse("GetAppRoles-InternalError", "An error occurred while determining your app roles, please try again later.");
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Error while processing request body: " + exc.ToString());
                return GetBlockPageApiResponse("GetAppRoles-InternalError", "An error occurred while determining your app roles, please try again later.");
            }
        }       


        private IActionResult GetContinueApiResponse(string code, string userMessage, string appRoles, string UserId = "")
        {
            return GetB2cApiConnectorResponse("Continue", code, userMessage, 200, appRoles, UserId);
        }

        private IActionResult GetValidationErrorApiResponse(string code, string userMessage)
        {
            return GetB2cApiConnectorResponse("ValidationError", code, userMessage, 400, null);
        }

        private IActionResult GetBlockPageApiResponse(string code, string userMessage)
        {
            return GetB2cApiConnectorResponse("ShowBlockPage", code, userMessage, 200, null);
        }

        private IActionResult GetB2cApiConnectorResponse(string action, string code, string userMessage, int statusCode, string appRoles, string UserId = "")
        {
            var responseProperties = new Dictionary<string, object>
            {
                { "version", "1.0.0" },
                { "action", action },
                { "userMessage", userMessage },
                { AppRolesAttributeName, appRoles },
                { AppUserIdAttributeName,UserId }
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
