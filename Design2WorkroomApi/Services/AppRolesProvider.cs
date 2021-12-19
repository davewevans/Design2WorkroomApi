using Design2WorkroomApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Services
{
    public class AppRolesProvider : IAppRolesProvider
    {
        public Task<string> GetAppRolesAsync(string objectId, string clientId)
        {
            throw new NotImplementedException();
        }
    }
}
