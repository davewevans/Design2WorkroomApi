using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.DTOs
{
    public record ProfileCreateDto
    {
        public string Email { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        public string PhonePrimary { get; init; } = string.Empty;

        public string PhoneSecondary { get; init; } = string.Empty;

        public string StreetAddress1 { get; set; } = string.Empty;

        public string StreetAddress2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string PostalCode { get; init; } = string.Empty;

        public string WorkroomName { get; init; } = string.Empty;

        public string ContactNamePrimary { get; init; } = string.Empty;

        public string ContactNameSecondary { get; init; } = string.Empty;

        public Guid AppUserId { get; init; }
    }
}
