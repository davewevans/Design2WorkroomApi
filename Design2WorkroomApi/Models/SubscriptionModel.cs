using Design2WorkroomApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table(name: "Subscriptions")]
    public class SubscriptionModel : Entity
    {
        public SubscriptionModel(string subscriptionName)
        {
            SubscriptionName = subscriptionName;
        }

        public string SubscriptionName { get; set; }

        public BillingCycle BillingCycle { get; set; }

        public DateTime SubscriptionStartedDate { get; set; }

        public DateTime SubscriptionEndedDate { get; set; }

        public DateTime SubscriptionPausedDate { get; set; }

        public SubscriptionStatus Status { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime RenewalDate { get; set; }

        public bool Paused { get; set; }

        public int? SubscriptionPlanId { get; set; }

        [ForeignKey(nameof(DesignerModel))]
        public Guid DesignerId { get; set; }

        public DesignerModel Designer { get; set; } = null!;
    }
}
