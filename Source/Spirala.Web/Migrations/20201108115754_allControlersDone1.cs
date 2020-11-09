using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class allControlersDone1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "RegistrationOfSoldier",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "RegistrationOfSoldier");
        }
    }
}
