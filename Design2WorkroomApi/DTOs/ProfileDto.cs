using Design2WorkroomApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.DTOs
{
    public record ProfileDto
    {
        public Guid Id { get; set; }
    
        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhonePrimary { get; set; } = string.Empty;

        public string PhoneSecondary { get; set; } = string.Empty;

        public string StreetAddress1 { get; set; } = string.Empty;

        public string StreetAddress2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;

        public string WorkroomName { get; set; } = string.Empty;

        public string ContactNamePrimary { get; set; } = string.Empty;

        public string ContactNameSecondary { get; set; } = string.Empty;

        public string ProfilePicUrl { get; set; } = string.Empty;

        public Guid AppUserId { get; set; }
    }
}
