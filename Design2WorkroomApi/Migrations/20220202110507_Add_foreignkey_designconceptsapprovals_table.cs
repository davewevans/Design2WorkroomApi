using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class Add_foreignkey_designconceptsapprovals_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DesignConceptsApprovals_DesignConceptId",
                table: "DesignConceptsApprovals",
                column: "DesignConceptId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignConceptsApprovals_DesignConcepts_DesignConceptId",
                table: "DesignConceptsApprovals",
                column: "DesignConceptId",
                principalTable: "DesignConcepts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignConceptsApprovals_DesignConcepts_DesignConceptId",
                table: "DesignConceptsApprovals");

            migrationBuilder.DropIndex(
                name: "IX_DesignConceptsApprovals_DesignConceptId",
                table: "DesignConceptsApprovals");
        }
    }
}
