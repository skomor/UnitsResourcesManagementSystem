using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class miasta4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "MilitaryUnit");

            migrationBuilder.AddColumn<int>(
                name: "MiastoID",
                table: "MilitaryUnit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryUnit_MiastoID",
                table: "MilitaryUnit",
                column: "MiastoID");

            migrationBuilder.AddForeignKey(
                name: "FK_MilitaryUnit_Miasto_MiastoID",
                table: "MilitaryUnit",
                column: "MiastoID",
                principalTable: "Miasto",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilitaryUnit_Miasto_MiastoID",
                table: "MilitaryUnit");

            migrationBuilder.DropIndex(
                name: "IX_MilitaryUnit_MiastoID",
                table: "MilitaryUnit");

            migrationBuilder.DropColumn(
                name: "MiastoID",
                table: "MilitaryUnit");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "MilitaryUnit",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
