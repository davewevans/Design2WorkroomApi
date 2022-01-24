using Design2WorkroomApi.Models;
using Microsoft.Graph;

namespace Design2WorkroomApi.Services.Contracts
{
    public interface IB2CGraphClient
    {
        Task<string> GetUserByObjectId(string objectId);

        Task<string> GetAllUsers(string query);

        Task<string> DeleteUser(string email);
        Task<string> CreateUser(string json);
        Task<string> UpdateUser(string objectId, string json);
        Task<string> RegisterExtension(string objectId, string body);
        Task<string> UnregisterExtension(string appObjectId, string extensionObjectId);
        Task<string> GetExtensions(string appObjectId);
        Task<string> GetApplications(string query);
    }
}
