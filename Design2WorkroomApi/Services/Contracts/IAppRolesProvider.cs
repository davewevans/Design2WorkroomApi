using Design2WorkroomApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinifyAPI;

namespace Design2WorkroomApi.Services.Contracts
{
    public interface IAppRolesProvider
{
        Task<(bool IsSuccess, string? AppUserRole, string? UserId, string? ErrorMessage)> GetAppRolesAsync(string email, string objectId);
        Task<(bool IsSuccess, User? userData, string? ErrorMessage)> GetAppRolesByobjectId(string objectId);
        Task<(bool IsSuccess, User? userData, string? ErrorMessage)> UserExistsAsync(string email);
        Task<(bool IsSuccess, User? userData, string? ErrorMessage)> updateUserObjectId(User user);
    }
}
