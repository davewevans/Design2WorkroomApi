using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table("WorkOrders")]
    public class WorkOrderModel : Entity
    {
        public int WorkOrderNumber { get; set; }

        public DateTime DateOrdered { get; set; }

        [ForeignKey(nameof(WorkroomModel))]
        public Guid? WorkroomId { get; set; }

        public WorkroomModel? Workroom { get; set; } = null!;


        [ForeignKey(nameof(ClientModel))]
        public Guid? ClientId { get; set; }

        public ClientModel? Client { get; set; } = null!;


        [ForeignKey(nameof(DesignerModel))]
        public Guid DesignerId { get; set; }

        public DesignerModel Designer { get; set; } = null!;

   

        public List<WorkOrderItemModel> WorkOrderItems { get; set; } = null!;
    }
}
