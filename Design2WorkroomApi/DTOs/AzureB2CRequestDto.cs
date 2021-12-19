using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Design2WorkroomApi.DTOs
{
    public class AzureB2CRequestDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("identities")]
        public List<Identity> Identities { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("objectId")]
        public string ObjectId { get; set; }

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("step")]
        public string Step { get; set; }

        [JsonPropertyName("ui_locales")]
        public string UILocales { get; set; }
    }

    public class Identity
    {
        [JsonPropertyName("signInType")]
        public string SignInType { get; set; }

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("issuerAssignedId")]
        public string IssuerAssignedId { get; set; }
    }
}
