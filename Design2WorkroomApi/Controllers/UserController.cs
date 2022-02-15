using AutoMapper;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Helpers;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Design2WorkroomApi.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace Design2WorkroomApi.Controllers
{
    [Route("api/userb2c")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepo;
        private readonly IB2CGraphClient _b2cUserHelper;
        private readonly IAppRolesProvider _appRolesProvider;
        private readonly AppUserHelper _appUserHelper;

        public UserController(ILogger<UserController> logger,
            IMapper mapper,
            IClientRepository clientRepo,
            IB2CGraphClient b2CGraphClient,
            IAppRolesProvider appRolesProvider,
            AppUserHelper appUserHelper,
            IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _clientRepo = clientRepo;
            _b2cUserHelper = b2CGraphClient;
            _appRolesProvider = appRolesProvider;
            _appUserHelper = appUserHelper;
            _config = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> createUser([FromBody] B2CUser b2cUser)
        {
            var createB2cUser = new Microsoft.Graph.User()
            {
                AccountEnabled = true,
                GivenName = b2cUser.FirstName,
                Surname = b2cUser.LastName,
                DisplayName = b2cUser.FirstName + " " + b2cUser.LastName,
                Identities = new List<ObjectIdentity>
                    {
                        new ObjectIdentity()
                        {
                            SignInType = "emailAddress",
                            Issuer = _config.GetValue<string>("AzureAdB2C:Domain"),
                            IssuerAssignedId = b2cUser.Email
                        }
                    },
                PasswordProfile = new PasswordProfile()
                {
                    Password = Helpers.PasswordHelper.GenerateNewPassword(4, 8, 4),
                    ForceChangePasswordNextSignIn = true
                },
                ODataType = null,
                PasswordPolicies = "DisablePasswordExpiration",
            };

            var response = await _b2cUserHelper.CreateUser(createB2cUser);
            if(response.IsSuccess)
            {
                return Ok(JsonConvert.SerializeObject(response.userObject));
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
            //return Ok(response);
        }

        [HttpGet("{objectId:guid}", Name = "GetUserRoleByObjectId")]
        public async Task<IActionResult> getUserRole(string objectId)
        {
            var response = await _appRolesProvider.GetAppRolesByobjectId(objectId);
            if (response.IsSuccess)
            {
                var userData = _mapper.Map<UserDto>(response.userData);
                return Ok(userData);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }

        [HttpGet("user/exists", Name = "UserExists")]
        public async Task<IActionResult> UserExists(string email)
        {
            var response = await _appRolesProvider.UserExistsAsync(email);
            if (response.IsSuccess)
            {
                if (response.userData != null)
                {
                    var userData = _mapper.Map<UserDto>(response.userData);
                    return Ok(userData);
                }
                else
                {
                    return NotFound(response.ErrorMessage);
                }
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }

        [HttpPost("updateUserObjectId")]
        public async Task<IActionResult> updateUserObjectId([FromBody] UserDto b2cUser)
        {
            var User = _mapper.Map<Models.User>(b2cUser);
            var response = await _appRolesProvider.updateUserObjectId(User);
            if (response.IsSuccess)
            {
                var userData = _mapper.Map<UserDto>(response.userData);
                return Ok(userData);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }
    }
}
