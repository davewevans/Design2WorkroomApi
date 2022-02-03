using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class ClientDesign_AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientDesign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDesign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientDesign_AppUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientDesign_AppUsers_DesignerId",
                        column: x => x.DesignerId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientDesign_ClientId",
                table: "ClientDesign",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDesign_DesignerId",
                table: "ClientDesign",
                column: "DesignerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientDesign");
        }
    }
}
