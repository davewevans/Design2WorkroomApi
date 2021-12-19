using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using TinifyAPI;

namespace Design2WorkroomApi.Enums
{
    public enum InvoiceStatus
    {
        Draft, // The invoice has been created, but it has not been sent to the client.
        Sent, // The invoice has been sent to the client.
        Viewed, // The invoice has been sent to the client.
        Partial, // The invoice has been partially paid.
        Paid, // The invoice has been paid in full.
        Overdue, // The invoice is past its due date with an amount due still outstanding.
        Canceled // The invoice has been manually marked as Canceled by the user.
    }

    public enum SubscriptionStatus
    {
        Active, // This means that we have received the payment of your subscription and it is now Active.
        PendingPayment, // This status indicates that the subscription has been created, but we are still waiting for the payment to hit our account.
        PendingCancellation, // When a subscription is manually canceled by the customer, its status is not usually transitioned to Cancelled immediately. This is because you have paid for a subscription until the end of the renewal date, so until the end of the pre-paid period, and you are entitled to use the subscription until then. When the pre-paid period ends, so right after the renewal is suppose to take place, the status of your subscription will change to Cancel.
        OnHold, // A subscription is placed On-Hold when an associated order is awaiting payment, or it has been manually suspended by the store owner or customer. A subscription can remain On-Hold indefinitely. If it was manually suspended, it will need to be manually reactivated. If it was suspended awaiting payment, it will be reactivated once that payment is processed.
        Canceled, // The Canceled status is assigned to subscriptions when they reach the end of their pre-paid term, so right after the renewal is suppose to take place but the customer has instead canceled his active subscription.  
    }

    public enum EmailStatus
    {
        Draft, // Email created but not sent.
        Sent, // Email sent but not opened.
        Received // Email received.
    }

    public enum ContractStatus
    {
        Draft,
        PendingSignature,
        Signed,
    }

    public enum BillingCycle
    {
        Monthly,
        Quarterly,
        Biannually, // every 6 months
        Yearly,
        Biennially, // every 2 years
        None
    }

    public enum AppUserRole
    {
        Admin,
        Designer,
        Client,
        Workroom,
        Unknown
    }

}
