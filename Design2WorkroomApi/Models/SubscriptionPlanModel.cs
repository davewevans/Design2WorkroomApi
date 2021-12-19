using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table("SubscriptionPlans")]
    public class SubscriptionPlanModel : Entity
    {

        public SubscriptionPlanModel(
            string sku, 
            string title, 
            string? shortDescription = null, 
            string? longDescription = null)
        {
            Sku = sku;
            Title = title;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
        }

        public string Sku { get; set; }

        public string Title { get; set; } 

        public string? ShortDescription { get; set; } 

        public string? LongDescription { get; set; } 

        [Column(TypeName = "decimal(18,4)")]
        public decimal? PricePerMonth { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? PricePerQuarter { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? PricePerBiAnnual { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? PricePerYear { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? PricePerBiennial { get; set; }

        public int FreeTrialDays { get; set; }

        public bool IsActive { get; set; }
    }
}
