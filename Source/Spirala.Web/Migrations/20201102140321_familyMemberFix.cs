using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class familyMemberFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceOfResidence",
                table: "FamilyMember",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceOfResidence",
                table: "FamilyMember");
        }
    }
}
