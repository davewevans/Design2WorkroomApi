using Microsoft.Graph;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Design2WorkroomApi.Models
{
    public class B2CUserModel : Microsoft.Graph.User
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public void SetB2CProfile(string TenantName)
        {
            this.PasswordProfile = new PasswordProfile
            {
                ForceChangePasswordNextSignIn = true,
                Password = this.Password,
                ODataType = null
            };
            this.PasswordPolicies =  "DisablePasswordExpiration,DisableStrongPassword";
            this.Password = null;
            this.ODataType = null;

            foreach (var item in this.Identities)
            {
                if (item.SignInType == "emailAddress" || item.SignInType == "userName")
                {
                    item.Issuer = TenantName;
                }
            }
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
