using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class table_Add_foreignkey_designconceptsapprovals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DesignConceptId",
                table: "DesignConceptsApprovals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesignConceptId",
                table: "DesignConceptsApprovals");
        }
    }
}
