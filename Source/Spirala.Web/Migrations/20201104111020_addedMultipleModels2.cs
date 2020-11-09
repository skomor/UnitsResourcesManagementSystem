using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aut3.Migrations
{
    public partial class addedMultipleModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Soldier",
                table: "Soldier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilyMember",
                table: "FamilyMember");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Soldier");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FamilyMember");

            migrationBuilder.DropColumn(
                name: "FamilyRelation",
                table: "FamilyMember");

            migrationBuilder.DropColumn(
                name: "RelatedToSoldierId",
                table: "FamilyMember");

            migrationBuilder.AddColumn<Guid>(
                name: "SoldierId",
                table: "Soldier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CurrUnitMilitaryUnitId",
                table: "Soldier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfBirth",
                table: "Soldier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FamilyMemberId",
                table: "FamilyMember",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "FamilyMember",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Soldier",
                table: "Soldier",
                column: "SoldierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilyMember",
                table: "FamilyMember",
                column: "FamilyMemberId");

            migrationBuilder.CreateTable(
                name: "FamilyRelationToSoldier",
                columns: table => new
                {
                    FamilyRelationToSoldierId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyMemberId = table.Column<Guid>(nullable: false),
                    SoldierId = table.Column<Guid>(nullable: false),
                    NameOfRelationToSoldier = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyRelationToSoldier", x => x.FamilyRelationToSoldierId);
                    table.ForeignKey(
                        name: "FK_FamilyRelationToSoldier_FamilyMember_FamilyMemberId",
                        column: x => x.FamilyMemberId,
                        principalTable: "FamilyMember",
                        principalColumn: "FamilyMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyRelationToSoldier_Soldier_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldier",
                        principalColumn: "SoldierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryUnit",
                columns: table => new
                {
                    MilitaryUnitId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UnitNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryUnit", x => x.MilitaryUnitId);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationOfSoldier",
                columns: table => new
                {
                    RegistrationOfSoldierId = table.Column<Guid>(nullable: false),
                    Place = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    UnitMilitaryUnitId = table.Column<Guid>(nullable: true),
                    SoldierId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationOfSoldier", x => x.RegistrationOfSoldierId);
                    table.ForeignKey(
                        name: "FK_RegistrationOfSoldier_Soldier_SoldierId",
                        column: x => x.SoldierId,
                        principalTable: "Soldier",
                        principalColumn: "SoldierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrationOfSoldier_MilitaryUnit_UnitMilitaryUnitId",
                        column: x => x.UnitMilitaryUnitId,
                        principalTable: "MilitaryUnit",
                        principalColumn: "MilitaryUnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    VehicleId = table.Column<Guid>(nullable: false),
                    Vin = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    LicensePlate = table.Column<string>(nullable: true),
                    CarType = table.Column<int>(nullable: false),
                    TransmissionConfig = table.Column<int>(nullable: false),
                    FuelConfig = table.Column<int>(nullable: false),
                    DateOfProduction = table.Column<DateTime>(type: "Date", nullable: false),
                    EngineCapacityLiters = table.Column<double>(nullable: false),
                    CurrUnitMilitaryUnitId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicle_MilitaryUnit_CurrUnitMilitaryUnitId",
                        column: x => x.CurrUnitMilitaryUnitId,
                        principalTable: "MilitaryUnit",
                        principalColumn: "MilitaryUnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Soldier_CurrUnitMilitaryUnitId",
                table: "Soldier",
                column: "CurrUnitMilitaryUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyRelationToSoldier_FamilyMemberId",
                table: "FamilyRelationToSoldier",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyRelationToSoldier_SoldierId",
                table: "FamilyRelationToSoldier",
                column: "SoldierId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationOfSoldier_SoldierId",
                table: "RegistrationOfSoldier",
                column: "SoldierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationOfSoldier_UnitMilitaryUnitId",
                table: "RegistrationOfSoldier",
                column: "UnitMilitaryUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_CurrUnitMilitaryUnitId",
                table: "Vehicle",
                column: "CurrUnitMilitaryUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Soldier_MilitaryUnit_CurrUnitMilitaryUnitId",
                table: "Soldier",
                column: "CurrUnitMilitaryUnitId",
                principalTable: "MilitaryUnit",
                principalColumn: "MilitaryUnitId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soldier_MilitaryUnit_CurrUnitMilitaryUnitId",
                table: "Soldier");

            migrationBuilder.DropTable(
                name: "FamilyRelationToSoldier");

            migrationBuilder.DropTable(
                name: "RegistrationOfSoldier");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "MilitaryUnit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Soldier",
                table: "Soldier");

            migrationBuilder.DropIndex(
                name: "IX_Soldier_CurrUnitMilitaryUnitId",
                table: "Soldier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilyMember",
                table: "FamilyMember");

            migrationBuilder.DropColumn(
                name: "SoldierId",
                table: "Soldier");

            migrationBuilder.DropColumn(
                name: "CurrUnitMilitaryUnitId",
                table: "Soldier");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                table: "Soldier");

            migrationBuilder.DropColumn(
                name: "FamilyMemberId",
                table: "FamilyMember");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "FamilyMember");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Soldier",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "FamilyMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "FamilyRelation",
                table: "FamilyMember",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedToSoldierId",
                table: "FamilyMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Soldier",
                table: "Soldier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilyMember",
                table: "FamilyMember",
                column: "Id");
        }
    }
}
