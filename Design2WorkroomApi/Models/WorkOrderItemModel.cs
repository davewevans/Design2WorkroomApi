using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table("WorkOrderItems")]
    public class WorkOrderItemModel : Entity
    {
        public WorkOrderItemModel(string item, string? description = null)
        {
            Item = item;
            Description = description;
        }

        public string Item { get; set; } 

        public string? Description { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public string Color { get; set; }

        public string Fabric { get; set; }

        [ForeignKey(nameof(WorkOrderModel))]
        public Guid? WorkOrderId { get; set; }

        public WorkOrderModel WorkOrder { get; set; } = null!;
    }
}
