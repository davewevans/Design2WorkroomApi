using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class ClientDesign_Remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientDesign");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientDesign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClientModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DesignerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DesignerModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
        }
    }
}
