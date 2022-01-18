using Design2WorkroomApi.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace Design2WorkroomApi.Models
{
    // Uses the table-per-hierarchy (TPH) pattern
    // ref: https://docs.microsoft.com/en-us/ef/core/modeling/inheritance

    [Table(name: "AppUsers")]
    public abstract class AppUserBase : Entity
    {
        protected AppUserBase(string userName, string b2CObjectId)
        {
            UserName = userName;
            B2CObjectId = b2CObjectId;
        }

        public string UserName { get; set; } 
        public string Password { get; set; }

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; }    

        // Admin, Designer, Client, Workroom
        public AppUserRole AppUserRole { get; set; }  

        public ProfileModel Profile { get; set; } = null!;

        // Used for relationship 
        // Self reference many-to-many relationship
        // ref: https://github.com/dotnet/efcore/issues/10698
        public List<AppUserAppUser> AppUserParents { get; set; } = null!;
        public List<AppUserAppUser> AppUserChildren { get; set; } = null!;
    }
}
