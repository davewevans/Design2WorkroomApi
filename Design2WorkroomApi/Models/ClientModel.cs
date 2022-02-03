using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Design2WorkroomApi.Models
{
    public class ClientModel : AppUserBase
    {
        public ClientModel(string userName, string b2CObjectId) : base(userName, b2CObjectId)
        {

        }

        public List<WorkOrderModel> Workorders { get; set; }

        public List<ClientDesignModel> ClientDesigns { get; set; } = null;

        public bool InvitationAccepted { get; set; }
    }
}
