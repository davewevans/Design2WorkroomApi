using Azure.Identity;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Services.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using AuthenticationResult = Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult;

namespace Design2WorkroomApi.Services
{
    public class B2CGraphClient: IB2CGraphClient
    {
        private readonly IConfiguration _config;
        public static string aadInstance = "https://login.microsoftonline.com/";
        public static string aadGraphResourceId = "https://graph.microsoft.com/.default";
        public static string aadGraphEndpoint = "https://graph.windows.net/";
        public static string aadGraphSuffix = "";
        public static string aadGraphVersion = "api-version=1.6";
        private string clientId { get; set; }
        private string clientSecret { get; set; }
        private string domain { get; set; }

        //private AuthenticationContext authContext;
        //private Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential credential;
        private GraphServiceClient graphClient;

        public B2CGraphClient(IConfiguration config)
        {
            _config = config;
            // The client_id, client_secret, and domain are pulled in from the App.config file
            this.clientId = config.GetValue<string>("AzureAdB2CGraph:ClientId");
            this.clientSecret = config.GetValue<string>("AzureAdB2CGraph:ClientSecret");
            this.domain = config.GetValue<string>("AzureAdB2CGraph:Domain");
            // Initialize the client credential auth provider
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var clientSecretCredential = new ClientSecretCredential(domain, clientId, clientSecret);
            graphClient = new GraphServiceClient(clientSecretCredential, scopes);
        }


        public async Task<(bool IsSuccess, string? userObjectId, string? ErrorMessage)> CreateUser(Microsoft.Graph.User User)
        {
            return await SendGraphPostRequest("/users", User);
        }
       

        private async Task<(bool IsSuccess, string? Clients, string? ErrorMessage)> SendGraphPostRequest(string api, Microsoft.Graph.User User)
        {
            try
            {
                Microsoft.Graph.User userResponse = await graphClient.Users.Request().AddAsync(User);
                if(userResponse != null)
                {
                    return (true, userResponse.Id, $"Error while creating User '{userResponse.DisplayName}' on Azure B2C.");
                }
                else
                {
                    return (false, null, $"Error while creating User '{User.DisplayName}' on Azure B2C.");
                }
                //return (false,null,$"Error while creating User '{User.DisplayName}' on Azure B2C.");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
            
        }

        
    }
}
