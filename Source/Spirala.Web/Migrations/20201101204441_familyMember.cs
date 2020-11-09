using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class familyMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamilyMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RelatedToSoldierId = table.Column<Guid>(nullable: false),
                    FamilyRelation = table.Column<int>(nullable: false),
                    FName = table.Column<string>(nullable: true),
                    LName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMember", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyMember");
        }
    }
}
