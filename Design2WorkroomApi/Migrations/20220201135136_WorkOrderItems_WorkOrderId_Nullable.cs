using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class WorkOrderItems_WorkOrderId_Nullable : Migration
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
