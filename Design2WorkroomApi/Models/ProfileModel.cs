using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table(name: "Profiles")]
    public class ProfileModel : Entity
    {
        public ProfileModel(
            string email, 
            string? firstName = null, 
            string? lastName = null, 
            string? phonePrimary = null, 
            string? phoneSecondary = null, 
            string? postalCode = null, 
            string? workroomName = null, 
            string? contactNamePrimary = null, 
            string? contactNameSecondary = null, 
            string? profilePicUrl = null)
        {
                Email = email;
                FirstName = firstName;
                LastName = lastName;
                PhonePrimary = phonePrimary;
                PhoneSecondary = phoneSecondary;
                PostalCode = postalCode;
                WorkroomName = workroomName;
                ContactNamePrimary = contactNamePrimary;
                ContactNameSecondary = contactNameSecondary;
                ProfilePicUrl = profilePicUrl;
        }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } 

        [MaxLength(128, ErrorMessage = "First Name has too many characters. 128 Max.")]
        public string? FirstName { get; set; } 

        [MaxLength(128, ErrorMessage = "Last Name has too many characters. 128 Max.")]
        public string? LastName { get; set; } 

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhonePrimary { get; set; } 

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneSecondary { get; set; }

        [MaxLength(128, ErrorMessage = "Street address has too many characters. 128 Max.")]
        public string? StreetAddress1 { get; set; }

        [MaxLength(128, ErrorMessage = "Street address has too many characters. 128 Max.")]
        public string? StreetAddress2 { get; set; }

        [MaxLength(128, ErrorMessage = "City has too many characters. 128 Max.")]
        public string? City { get; set; }

        [MaxLength(128, ErrorMessage = "State has too many characters. 128 Max.")]
        public string? State { get; set; }

        [MaxLength(128, ErrorMessage = "PostalCode has too many characters. 128 Max.")]
        public string? PostalCode { get; set; }

        [MaxLength(128, ErrorMessage = "Country code has too many characters. 128 Max.")]
        public string? CountryCode { get; set; }

        [MaxLength(128, ErrorMessage = "Workroom Name has too many characters. 128 Max.")]
        public string? WorkroomName { get; set; }

        [MaxLength(128, ErrorMessage = "Contact Name Primary has too many characters. 128 Max.")]
        public string? ContactNamePrimary { get; set; } 

        [MaxLength(128, ErrorMessage = "Contact Name Secondary has too many characters. 128 Max.")]
        public string? ContactNameSecondary { get; set; } 

        public string? ProfilePicUrl { get; set; } 

        [ForeignKey(nameof(AppUserBase))]
        public Guid AppUserId { get; set; }

        public AppUserBase AppUser { get; set; } = null!;

    }
}
