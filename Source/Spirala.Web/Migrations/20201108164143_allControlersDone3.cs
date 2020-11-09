using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class allControlersDone3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "EngineCapacityLiters",
                table: "Vehicle",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "EngineCapacityLiters",
                table: "Vehicle",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
