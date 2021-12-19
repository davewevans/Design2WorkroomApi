using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    /// <summary>
    /// Bridge table used for many-to-many self-referencing relationship with AppUsers.
    /// Used for many-to-many relationship between Designers-Clients and Designers-Workrooms.
    /// Designer, Client, and Workroom models use the table-per-hierarchy pattern hence 
    /// the need for a self-referencing relationship.
    /// ref: https://docs.microsoft.com/en-us/ef/core/modeling/inheritance
    /// </summary>
    [Table("AppUserAppUsers")]
    public class AppUserAppUser
    {
        public Guid AppUserParentId { get; set; }

        public Guid AppUserChildId { get; set; }

        public bool Active { get; set; }

        public AppUserBase ParentAppUser { get; set; } = null!;

        public AppUserBase ChildAppUser { get; set; } = null!;
    }
}
