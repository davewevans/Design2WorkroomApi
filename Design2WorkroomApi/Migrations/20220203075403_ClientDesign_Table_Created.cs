using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class ClientDesign_Table_Created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderItems_WorkOrders_WorkOrderId",
                table: "WorkOrderItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkOrderId",
                table: "WorkOrderItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "ClientDesign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    ClientModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignerModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDesign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientDesign_AppUsers_ClientModelId",
                        column: x => x.ClientModelId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientDesign_AppUsers_DesignerModelId",
                        column: x => x.DesignerModelId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientDesign_ClientModelId",
                table: "ClientDesign",
                column: "ClientModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDesign_DesignerModelId",
                table: "ClientDesign",
                column: "DesignerModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderItems_WorkOrders_WorkOrderId",
                table: "WorkOrderItems",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderItems_WorkOrders_WorkOrderId",
                table: "WorkOrderItems");

            migrationBuilder.DropTable(
                name: "ClientDesign");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkOrderId",
                table: "WorkOrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderItems_WorkOrders_WorkOrderId",
                table: "WorkOrderItems",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
