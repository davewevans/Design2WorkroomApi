using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class MinorTypeFix1_22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_AppUsers_WorkRoomId",
                table: "WorkOrders");

            migrationBuilder.RenameColumn(
                name: "WorkRoomId",
                table: "WorkOrders",
                newName: "WorkroomId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_WorkRoomId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_WorkroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_AppUsers_WorkroomId",
                table: "WorkOrders",
                column: "WorkroomId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_AppUsers_WorkroomId",
                table: "WorkOrders");

            migrationBuilder.RenameColumn(
                name: "WorkroomId",
                table: "WorkOrders",
                newName: "WorkRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_WorkroomId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_WorkRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_AppUsers_WorkRoomId",
                table: "WorkOrders",
                column: "WorkRoomId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
