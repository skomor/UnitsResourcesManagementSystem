using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class unitInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
