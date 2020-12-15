using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class unitNumberUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MilitaryUnit_UnitMilitaryUnitId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UnitMilitaryUnitId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UnitMilitaryUnitId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UnitNumber",
                table: "MilitaryUnit",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryUnit_UnitNumber",
                table: "MilitaryUnit",
                column: "UnitNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MilitaryUnit_UnitNumber",
                table: "MilitaryUnit");

            migrationBuilder.AlterColumn<string>(
                name: "UnitNumber",
                table: "MilitaryUnit",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitMilitaryUnitId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UnitMilitaryUnitId",
                table: "AspNetUsers",
                column: "UnitMilitaryUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MilitaryUnit_UnitMilitaryUnitId",
                table: "AspNetUsers",
                column: "UnitMilitaryUnitId",
                principalTable: "MilitaryUnit",
                principalColumn: "MilitaryUnitId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
