using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class usunietoMiasto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilitaryUnit_Powiat_PowiatID",
                table: "MilitaryUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_MilitaryUnit_CurrUnitMilitaryUnitId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_CurrUnitMilitaryUnitId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CurrUnitMilitaryUnitId",
                table: "Vehicle");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrUnitID",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "PowiatID",
                table: "MilitaryUnit",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_CurrUnitID",
                table: "Vehicle",
                column: "CurrUnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_MilitaryUnit_Powiat_PowiatID",
                table: "MilitaryUnit",
                column: "PowiatID",
                principalTable: "Powiat",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_MilitaryUnit_CurrUnitID",
                table: "Vehicle",
                column: "CurrUnitID",
                principalTable: "MilitaryUnit",
                principalColumn: "MilitaryUnitId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilitaryUnit_Powiat_PowiatID",
                table: "MilitaryUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_MilitaryUnit_CurrUnitID",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_CurrUnitID",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CurrUnitID",
                table: "Vehicle");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrUnitMilitaryUnitId",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PowiatID",
                table: "MilitaryUnit",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_CurrUnitMilitaryUnitId",
                table: "Vehicle",
                column: "CurrUnitMilitaryUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_MilitaryUnit_Powiat_PowiatID",
                table: "MilitaryUnit",
                column: "PowiatID",
                principalTable: "Powiat",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_MilitaryUnit_CurrUnitMilitaryUnitId",
                table: "Vehicle",
                column: "CurrUnitMilitaryUnitId",
                principalTable: "MilitaryUnit",
                principalColumn: "MilitaryUnitId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
