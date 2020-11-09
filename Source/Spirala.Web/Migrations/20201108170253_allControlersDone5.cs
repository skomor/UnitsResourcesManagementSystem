using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class allControlersDone5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EngineCapacityLiters",
                table: "Vehicle",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EngineCapacityLiters",
                table: "Vehicle",
                type: "decimal",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
