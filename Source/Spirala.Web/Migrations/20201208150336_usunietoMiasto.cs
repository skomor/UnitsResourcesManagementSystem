using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class usunietoMiasto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilitaryUnit_Miasto_MiastoID",
                table: "MilitaryUnit");

            migrationBuilder.RenameColumn(
                name: "MiastoID",
                table: "MilitaryUnit",
                newName: "PowiatID");

            migrationBuilder.RenameIndex(
                name: "IX_MilitaryUnit_MiastoID",
                table: "MilitaryUnit",
                newName: "IX_MilitaryUnit_PowiatID");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConsumedTime",
                table: "PersistedGrants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PersistedGrants",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "PersistedGrants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Miasto",
                table: "MilitaryUnit",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DeviceCodes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "DeviceCodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.AddForeignKey(
                name: "FK_MilitaryUnit_Powiat_PowiatID",
                table: "MilitaryUnit",
                column: "PowiatID",
                principalTable: "Powiat",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MilitaryUnit_Powiat_PowiatID",
                table: "MilitaryUnit");

            migrationBuilder.DropIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "ConsumedTime",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "Miasto",
                table: "MilitaryUnit");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DeviceCodes");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "DeviceCodes");

            migrationBuilder.RenameColumn(
                name: "PowiatID",
                table: "MilitaryUnit",
                newName: "MiastoID");

            migrationBuilder.RenameIndex(
                name: "IX_MilitaryUnit_PowiatID",
                table: "MilitaryUnit",
                newName: "IX_MilitaryUnit_MiastoID");

            migrationBuilder.AddForeignKey(
                name: "FK_MilitaryUnit_Miasto_MiastoID",
                table: "MilitaryUnit",
                column: "MiastoID",
                principalTable: "Miasto",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
