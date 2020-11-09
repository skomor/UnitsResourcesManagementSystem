using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class allControlersDone6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineCapacityLiters",
                table: "Vehicle");

            migrationBuilder.AddColumn<int>(
                name: "EngineCapacityCC",
                table: "Vehicle",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineCapacityCC",
                table: "Vehicle");

            migrationBuilder.AddColumn<int>(
                name: "EngineCapacityLiters",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
