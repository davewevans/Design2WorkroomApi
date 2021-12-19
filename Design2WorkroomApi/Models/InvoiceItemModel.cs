using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table(name: "InvoiceItems")]
    public class InvoiceItemModel : Entity
    { 
        public InvoiceItemModel(string item, string? description = null)
        {
            Item = item;    
            Description = description;
        }

        public string Item { get; set; }

        public string? Description { get; set; } 

        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(InvoiceModel))]
        public Guid InvoiceId { get; set; }

        public InvoiceModel Invoice { get; set; } = null!;
    }
}
