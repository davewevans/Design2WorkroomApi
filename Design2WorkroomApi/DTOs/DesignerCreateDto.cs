using Design2WorkroomApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.DTOs
{
    public record DesignerCreateDto
    {      
        public string UserName { get; set; } = string.Empty;

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; } = string.Empty;

        // Admin, Designer, Client, Workroom
        public string UserRole { get; set; } = string.Empty;

        public AppUserRole AppUserType { get; init; }

        public ProfileDto Profile { get; set; } = null!;
    }
}
