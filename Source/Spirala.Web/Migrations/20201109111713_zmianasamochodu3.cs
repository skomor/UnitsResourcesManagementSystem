using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class zmianasamochodu3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOfRelationToSoldier",
                table: "FamilyRelationToSoldier");

            migrationBuilder.AddColumn<int>(
                name: "RelationToSoldier",
                table: "FamilyRelationToSoldier",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelationToSoldier",
                table: "FamilyRelationToSoldier");

            migrationBuilder.AddColumn<int>(
                name: "NameOfRelationToSoldier",
                table: "FamilyRelationToSoldier",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
