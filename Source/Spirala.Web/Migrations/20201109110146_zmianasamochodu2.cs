using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class zmianasamochodu2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Vehicle");

            migrationBuilder.AddColumn<int>(
                name: "WeightKg",
                table: "Vehicle",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeightKg",
                table: "Vehicle");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
