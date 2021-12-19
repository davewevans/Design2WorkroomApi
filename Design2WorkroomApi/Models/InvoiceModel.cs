using Design2WorkroomApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table("Invoices")]
    public class InvoiceModel : Entity
    {
        public InvoiceModel(
            string? billingState = null, 
            string? postalCode = null, 
            string? billingCountry = null, 
            string? notes = null)
        {
                BillingState = billingState;    
                PostalCode = postalCode;
                BillingState = billingCountry;
                Notes = notes;
        }

        public int InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal BalanceDue { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal AmountPaid { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Tax { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }

        public int? GracePeriodInDays { get; set; }

        public string? BillingState { get; set; }

        public string? PostalCode { get; set; }

        public string? BillingCountry { get; set; } 

        public string? Notes { get; set; } 

        public InvoiceStatus Status { get; set; }

        [ForeignKey(nameof(SubscriptionModel))]
        public Guid SubscriptionId { get; set; }

        public SubscriptionModel Subscription { get; set; } = null!;

        [ForeignKey(nameof(DesignerModel))]
        public Guid? DesignerId { get; set; }

        public DesignerModel Designer { get; set; } = null!;

        public List<InvoiceItemModel> InvoiceItems { get; set; } = null!;

    }
}
