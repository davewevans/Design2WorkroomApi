using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    public class WorkroomModel : AppUserBase
    {
        public WorkroomModel(string userName, string b2CObjectId) : base(userName, b2CObjectId)
        {
        }

        public List<WorkOrderModel> Workorders { get; set; }

        public bool InvitationAccepted { get; set; }
    }
}
