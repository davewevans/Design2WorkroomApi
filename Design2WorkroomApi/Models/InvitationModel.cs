using Design2WorkroomApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table(name: "Invitations")]
    public class InvitationModel : Entity
    {
        public InvitationModel(string invitationCode, string inviteeEmail, string inviteeFirstName, string inviteeLastName)
        {
            InvitationCode = invitationCode;
            InviteeEmail = inviteeEmail;
            InviteeFirstName = inviteeFirstName;
            InviteeLastName = inviteeLastName;
        }

        public Guid Id { get; set; }

        public string InvitationCode { get; set; } 

        public string InviteeEmail { get; set; } 

        public string InviteeFirstName { get; set; } 

        public string InviteeLastName { get; set; } 

        public Guid DesignerId { get; set; }

        public bool IsComplete { get; set; }

        public AppUserRole Role { get; set; }
    }
}
