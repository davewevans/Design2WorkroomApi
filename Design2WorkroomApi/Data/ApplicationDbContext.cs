using Design2WorkroomApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Design2WorkroomApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure the name and type of the discriminator column
            // ref: https://docs.microsoft.com/en-us/ef/core/modeling/inheritance
            builder.Entity<AppUserBase>()
              .HasDiscriminator(x => x.AppUserRole)
              .HasValue<AdminModel>(Enums.AppUserRole.Admin)
              .HasValue<DesignerModel>(Enums.AppUserRole.Designer)
              .HasValue<ClientModel>(Enums.AppUserRole.Client)
              .HasValue<WorkroomModel>(Enums.AppUserRole.Workroom);

            builder.Entity<AppUserBase>()
                .Property(e => e.AppUserRole)
                .HasColumnName("AppUserRole");

            // Configure one-to-one relationships
            builder.Entity<AppUserBase>()
             .HasOne(a => a.Profile)
             .WithOne(b => b.AppUser)
             .HasForeignKey<ProfileModel>(b => b.AppUserId);

            // Configure self-reference in many-to-many relationship with AppUsers
            // ref: https://github.com/dotnet/efcore/issues/10698
            builder.Entity<AppUserAppUser>()
                .HasKey(u => new { u.AppUserChildId, u.AppUserParentId });

            builder.Entity<AppUserBase>()
                .HasMany(u => u.AppUserChildren)
                .WithOne(f => f.ChildAppUser)
                .HasForeignKey(f => f.AppUserChildId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<AppUserBase>()
                .HasMany(u => u.AppUserParents)
                .WithOne(f => f.ParentAppUser)
                .HasForeignKey(f => f.AppUserParentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure one-to-many relationships
            builder.Entity<WorkOrderModel>()
                .HasOne(p => p.Client)
                .WithMany(b => b.Workorders)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<WorkOrderModel>()
                .HasOne(p => p.Workroom)
                .WithMany(b => b.Workorders)
                .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(builder);
        }

        public DbSet<AppUserBase> AppUsers { get; set; } = default!;
        public DbSet<AppUserAppUser> AppUserAppUsers { get; set; } = default!;
        public DbSet<ColorModel> Colors { get; set; } = default!;
        public DbSet<ContractModel> Contracts { get; set; } = default!;
        public DbSet<DesignSpecificationModel> DesignSpecifications { get; set; } = default!;
        public DbSet<DimensionsModel> Dimensions { get; set; } = default!;
        public DbSet<EmailModel> Emails { get; set; } = default!;
        public DbSet<FabricModel> Fabrics { get; set; } = default!;
        public DbSet<InvoiceItemModel> InvoiceItems { get; set; } = default!;
        public DbSet<InvoiceModel> Invoices { get; set; } = default!;
        public DbSet<NotificationModel> Notifications { get; set; } = default!;
        public DbSet<ReviewModel> Reviews { get; set; } = default!;
        public DbSet<StyleModel> Styles { get; set; } = default!;
        public DbSet<SubscriptionModel> Subscriptions { get; set; } = default!;
        public DbSet<SubscriptionPlanModel> SubscriptionPlans { get; set; } = default!;
        public DbSet<WorkOrderItemModel> WorkOrderItems { get; set; } = default!;
        public DbSet<WorkOrderModel> WorkOrders { get; set; } = default!;
        public DbSet<DesignConceptModel> DesignConcepts { get; set; } = default!;
        public DbSet<DesignConceptsApprovalModel> DesignConceptsApprovals { get; set; } = default!;
        public DbSet<InvitationModel> Invitations { get; set; } = default!;
        public DbSet<AttachmentsModel> Attachments { get; set; } = default!;
    }
}
