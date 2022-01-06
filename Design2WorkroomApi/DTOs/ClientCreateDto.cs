﻿using Design2WorkroomApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.DTOs
{
    public record ClientCreateDto
    {
        public string UserName { get; init; } = string.Empty;

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; } = string.Empty;

        // Admin, Designer, Client, Workroom
        public string UserRole { get; set; } = string.Empty;

        public Guid? DesignerId { get; init; }

        public AppUserRole AppUserType { get; init; }

        public ProfileDto Profile { get; set; } = null!;

        public bool InvitationAccepted { get; set; }
    }
}
