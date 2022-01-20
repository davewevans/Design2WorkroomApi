using Design2WorkroomApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.DTOs
{
    public record ClientDto
    {
        public Guid Id { get; init; }

        public string UserName { get; init; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; } = string.Empty;

        // Admin, Designer, Client, Workroom
        public AppUserRole AppUserRole { get; init; }

        public bool Active { get; set; }

        public ProfileDto Profile { get; set; } = null!;

        public bool InvitationAccepted { get; set; }
    }
}
