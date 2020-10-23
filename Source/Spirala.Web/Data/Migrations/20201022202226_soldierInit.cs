using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Data.Migrations
{
    public partial class soldierInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Soldier",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FName = table.Column<string>(nullable: true),
                    LName = table.Column<string>(nullable: true),
                    Pesel = table.Column<string>(nullable: true),
                    Sex = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soldier", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Soldier");
        }
    }
}
