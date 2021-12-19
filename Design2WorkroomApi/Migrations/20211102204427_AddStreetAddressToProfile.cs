using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Design2WorkroomApi.Migrations
{
    public partial class AddStreetAddressToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Profiles",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Profiles",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress1",
                table: "Profiles",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress2",
                table: "Profiles",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "StreetAddress1",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "StreetAddress2",
                table: "Profiles");
        }
    }
}
