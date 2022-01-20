using Design2WorkroomApi.Enums;
using Design2WorkroomApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.DTOs
{
    public record DesignerDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        // Azure B2C Object Id
        // Identifies user in Azure B2C
        public string B2CObjectId { get; set; } = string.Empty;

        // Admin, Designer, Client, Workroom
        public AppUserRole AppUserRole { get; set; }

        public ProfileDto Profile { get; set; } = null!;

        public SubscriptionModel Subscription { get; set; } = null!;

        public List<InvoiceModel> Invoices { get; set; } = null!;

        public List<WorkOrderModel> WorkOrders { get; set; } = null!;

        public List<EmailModel> Emails { get; set; } = null!;

        public List<DesignSpecificationModel> DesignSpecifications { get; set; } = null!;

        public List<ContractModel> Contracts { get; set; } = null!;

        public List<NotificationModel> Notifications { get; set; } = null!;
    }
}