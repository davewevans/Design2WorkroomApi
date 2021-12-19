using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinifyAPI;

namespace Design2WorkroomApi.Services.Contracts
{
    public interface IAppRolesProvider
{
        Task<string> GetAppRolesAsync(string objectId, string clientId);
    }
}
