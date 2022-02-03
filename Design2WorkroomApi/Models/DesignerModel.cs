using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    public class DesignerModel : AppUserBase
    {
        public DesignerModel(string userName, string b2CObjectId) : base(userName, b2CObjectId)
        {
        }

        public SubscriptionModel Subscription { get; set; } = null!;

        public List<InvoiceModel> Invoices { get; set; } = null!;

        public List<WorkOrderModel> WorkOrders { get; set; } = null!;

        public List<EmailModel> Emails { get; set; } = null!;

        public List<DesignSpecificationModel> DesignSpecifications { get; set; } = null!;

        public List<ContractModel> Contracts { get; set; } = null!;

        public List<NotificationModel> Notifications { get; set; } = null!;
        public List<ClientDesignModel> ClientDesigns { get; set; } = null!;
    }
}
