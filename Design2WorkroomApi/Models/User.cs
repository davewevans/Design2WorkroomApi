﻿using Design2WorkroomApi.Enums;

namespace Design2WorkroomApi.Models
{
    public class User
    {
        
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; } = string.Empty;

        // Admin, Designer, Client, Workroom
        public AppUserRole AppUserRole { get; set; }

        public ProfileModel Profile { get; set; } = null!;
    
    }
}
