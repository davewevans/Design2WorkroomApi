using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinifyAPI;

namespace Design2WorkroomApi.Services.Contracts
{
    public interface IAppRolesProvider
{
        Task<(bool IsSuccess, string? AppUserRole, string? ErrorMessage)> GetAppRolesAsync(string email, string objectId);
    }
}
