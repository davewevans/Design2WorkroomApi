using Design2WorkroomApi.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Helpers
{
    public class AppUserHelper
    {
        private readonly ILogger<AppUserHelper> _logger;

        public AppUserHelper(ILogger<AppUserHelper> logger)
        {
            _logger = logger;
        }

        public AppUserRole GetAppUserRole(string role)
        {
            switch (role.ToLower())
            {
                case "admin":
                    return AppUserRole.Admin;
                case "designer":
                    return AppUserRole.Designer;
                case "client":
                    return AppUserRole.Client;
                case "workroom":
                    return AppUserRole.Workroom;
                default:
                    return AppUserRole.Unknown;
            }
        }

    }
}
