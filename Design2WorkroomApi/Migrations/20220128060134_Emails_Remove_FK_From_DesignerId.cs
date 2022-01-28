using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class Emails_Remove_FK_From_DesignerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_AppUsers_DesignerId",
                table: "Emails");

            migrationBuilder.AlterColumn<Guid>(
                name: "DesignerId",
                table: "Emails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_AppUsers_DesignerId",
                table: "Emails",
                column: "DesignerId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_AppUsers_DesignerId",
                table: "Emails");

            migrationBuilder.AlterColumn<Guid>(
                name: "DesignerId",
                table: "Emails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_AppUsers_DesignerId",
                table: "Emails",
                column: "DesignerId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
