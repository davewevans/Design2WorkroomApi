using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class AddedOneToManyWithClientsWorkroomAndWorkorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "WorkOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkRoomId",
                table: "WorkOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ClientId",
                table: "WorkOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkRoomId",
                table: "WorkOrders",
                column: "WorkRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_AppUsers_ClientId",
                table: "WorkOrders",
                column: "ClientId",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_AppUsers_WorkRoomId",
                table: "WorkOrders",
                column: "WorkRoomId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_AppUsers_ClientId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_AppUsers_WorkRoomId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_ClientId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_WorkRoomId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "WorkRoomId",
                table: "WorkOrders");
        }
    }
}
