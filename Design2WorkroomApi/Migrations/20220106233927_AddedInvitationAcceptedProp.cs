using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class AddedInvitationAcceptedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InvitationAccepted",
                table: "AppUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WorkroomModel_InvitationAccepted",
                table: "AppUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitationAccepted",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "WorkroomModel_InvitationAccepted",
                table: "AppUsers");
        }
    }
}
