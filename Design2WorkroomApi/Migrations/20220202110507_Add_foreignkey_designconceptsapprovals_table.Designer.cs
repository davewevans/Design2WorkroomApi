﻿// <auto-generated />
using System;
using Design2WorkroomApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220202110507_Add_foreignkey_designconceptsapprovals_table")]
    partial class Add_foreignkey_designconceptsapprovals_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Design2WorkroomApi.Models.AppUserAppUser", b =>
                {
                    b.Property<Guid>("AppUserChildId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.HasKey("AppUserChildId", "AppUserParentId");

                    b.HasIndex("AppUserParentId");

                    b.ToTable("AppUserAppUsers");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.AppUserBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AppUserRole")
                        .HasColumnType("int")
                        .HasColumnName("AppUserRole");

                    b.Property<string>("B2CObjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasDiscriminator<int>("AppUserRole");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.AttachmentsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ContentLength")
                        .HasColumnType("bigint");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.ColorModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.ContractModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WorkroomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DesignerId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.DesignConceptModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DesignConcepts");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.DesignConceptsApprovalModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DesignConceptId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DesignConceptId");

                    b.ToTable("DesignConceptsApprovals");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.DesignSpecificationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DesignerId");

                    b.ToTable("DesignSpecifications");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.DimensionsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Dimensions");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.EmailModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReceived")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateSent")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FromEmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HtmlBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageStream")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToEmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("WorkroomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DesignerId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.FabricModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Fabrics");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.InvitationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InvitationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InviteeEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InviteeFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InviteeLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.InvoiceItemModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceItems");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.InvoiceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("BalanceDue")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("BillingCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BillingState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GracePeriodInDays")
                        .HasColumnType("int");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InvoiceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,4)");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DesignerId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.NotificationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WorkroomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DesignerId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.ProfileModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ContactNamePrimary")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ContactNameSecondary")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("CountryCode")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("LastName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PhonePrimary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneSecondary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProfilePicUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("StreetAddress1")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("StreetAddress2")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WorkroomName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.ReviewModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DesignerId")
                        .HasColumnType("int");

                    b.Property<int?>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.StyleModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Styles");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.SubscriptionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BillingCycle")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Paused")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RenewalDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubscriptionEndedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubscriptionPausedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SubscriptionPlanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubscriptionStartedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DesignerId")
                        .IsUnique();

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.SubscriptionPlanModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FreeTrialDays")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PricePerBiAnnual")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal?>("PricePerBiennial")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal?>("PricePerMonth")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal?>("PricePerQuarter")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal?>("PricePerYear")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionPlans");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.WorkOrderItemModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fabric")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.Property<Guid>("WorkOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WorkOrderId");

                    b.ToTable("WorkOrderItems");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.WorkOrderModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOrdered")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DesignerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkOrderNumber")
                        .HasColumnType("int");

                    b.Property<Guid?>("WorkroomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DesignerId");

                    b.HasIndex("WorkroomId");

                    b.ToTable("WorkOrders");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.AdminModel", b =>
                {
                    b.HasBaseType("Design2WorkroomApi.Models.AppUserBase");

                    b.ToTable("AppUsers");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.ClientModel", b =>
                {
                    b.HasBaseType("Design2WorkroomApi.Models.AppUserBase");

                    b.Property<bool>("InvitationAccepted")
                        .HasColumnType("bit");

                    b.ToTable("AppUsers");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.DesignerModel", b =>
                {
                    b.HasBaseType("Design2WorkroomApi.Models.AppUserBase");

                    b.ToTable("AppUsers");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.WorkroomModel", b =>
                {
                    b.HasBaseType("Design2WorkroomApi.Models.AppUserBase");

                    b.Property<bool>("InvitationAccepted")
                        .HasColumnType("bit")
                        .HasColumnName("WorkroomModel_InvitationAccepted");

                    b.ToTable("AppUsers");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.AppUserAppUser", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.AppUserBase", "ChildAppUser")
                        .WithMany("AppUserChildren")
                        .HasForeignKey("AppUserChildId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Design2WorkroomApi.Models.AppUserBase", "ParentAppUser")
                        .WithMany("AppUserParents")
                        .HasForeignKey("AppUserParentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ChildAppUser");

                    b.Navigation("ParentAppUser");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.ContractModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.DesignerModel", "Designer")
                        .WithMany("Contracts")
                        .HasForeignKey("DesignerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Designer");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.DesignConceptsApprovalModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.DesignConceptModel", "DesignConcept")
                        .WithMany("DesignConceptsApproval")
                        .HasForeignKey("DesignConceptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DesignConcept");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.DesignSpecificationModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.DesignerModel", "Designer")
                        .WithMany("DesignSpecifications")
                        .HasForeignKey("DesignerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Designer");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.EmailModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.DesignerModel", "Designer")
                        .WithMany("Emails")
                        .HasForeignKey("DesignerId");

                    b.Navigation("Designer");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.InvoiceItemModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.InvoiceModel", "Invoice")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.InvoiceModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.DesignerModel", "Designer")
                        .WithMany("Invoices")
                        .HasForeignKey("DesignerId");

                    b.HasOne("Design2WorkroomApi.Models.SubscriptionModel", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Designer");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.NotificationModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.DesignerModel", "Designer")
                        .WithMany("Notifications")
                        .HasForeignKey("DesignerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Designer");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.ProfileModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.AppUserBase", "AppUser")
                        .WithOne("Profile")
                        .HasForeignKey("Design2WorkroomApi.Models.ProfileModel", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.SubscriptionModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.DesignerModel", "Designer")
                        .WithOne("Subscription")
                        .HasForeignKey("Design2WorkroomApi.Models.SubscriptionModel", "DesignerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Designer");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.WorkOrderItemModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.WorkOrderModel", "WorkOrder")
                        .WithMany("WorkOrderItems")
                        .HasForeignKey("WorkOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkOrder");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.WorkOrderModel", b =>
                {
                    b.HasOne("Design2WorkroomApi.Models.ClientModel", "Client")
                        .WithMany("Workorders")
                        .HasForeignKey("ClientId");

                    b.HasOne("Design2WorkroomApi.Models.DesignerModel", "Designer")
                        .WithMany("WorkOrders")
                        .HasForeignKey("DesignerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Design2WorkroomApi.Models.WorkroomModel", "Workroom")
                        .WithMany("Workorders")
                        .HasForeignKey("WorkroomId");

                    b.Navigation("Client");

                    b.Navigation("Designer");

                    b.Navigation("Workroom");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.AppUserBase", b =>
                {
                    b.Navigation("AppUserChildren");

                    b.Navigation("AppUserParents");

                    b.Navigation("Profile")
                        .IsRequired();
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.DesignConceptModel", b =>
                {
                    b.Navigation("DesignConceptsApproval");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.InvoiceModel", b =>
                {
                    b.Navigation("InvoiceItems");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.WorkOrderModel", b =>
                {
                    b.Navigation("WorkOrderItems");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.ClientModel", b =>
                {
                    b.Navigation("Workorders");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.DesignerModel", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("DesignSpecifications");

                    b.Navigation("Emails");

                    b.Navigation("Invoices");

                    b.Navigation("Notifications");

                    b.Navigation("Subscription")
                        .IsRequired();

                    b.Navigation("WorkOrders");
                });

            modelBuilder.Entity("Design2WorkroomApi.Models.WorkroomModel", b =>
                {
                    b.Navigation("Workorders");
                });
#pragma warning restore 612, 618
        }
    }
}
