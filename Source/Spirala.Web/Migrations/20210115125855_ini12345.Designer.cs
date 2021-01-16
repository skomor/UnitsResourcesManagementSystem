﻿// <auto-generated />
using System;
using Aut3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aut3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210115125855_ini12345")]
    partial class ini12345
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Aut3.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Aut3.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CountyID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VoivodeshipID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CountyID");

                    b.HasIndex("VoivodeshipID");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Aut3.Models.County", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VoivodeshipID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("VoivodeshipID");

                    b.ToTable("County");
                });

            modelBuilder.Entity("Aut3.Models.FamilyMember", b =>
                {
                    b.Property<Guid>("FamilyMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("Date");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfResidence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.HasKey("FamilyMemberId");

                    b.ToTable("FamilyMember");
                });

            modelBuilder.Entity("Aut3.Models.FamilyRelationToSoldier", b =>
                {
                    b.Property<Guid>("FamilyRelationToSoldierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FamilyMemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RelationToSoldier")
                        .HasColumnType("int");

                    b.Property<Guid>("SoldierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FamilyRelationToSoldierId");

                    b.HasIndex("FamilyMemberId");

                    b.HasIndex("SoldierId");

                    b.ToTable("FamilyRelationToSoldier");
                });

            modelBuilder.Entity("Aut3.Models.MilitaryUnit", b =>
                {
                    b.Property<Guid>("MilitaryUnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountyID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UnitNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MilitaryUnitId");

                    b.HasIndex("CountyID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UnitNumber")
                        .IsUnique();

                    b.ToTable("MilitaryUnit");
                });

            modelBuilder.Entity("Aut3.Models.RegistrationOfSoldier", b =>
                {
                    b.Property<Guid>("RegistrationOfSoldierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfRegistration")
                        .HasColumnType("Date");

                    b.Property<Guid>("MilitaryUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SoldierId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RegistrationOfSoldierId");

                    b.HasIndex("MilitaryUnitId");

                    b.HasIndex("SoldierId")
                        .IsUnique();

                    b.ToTable("RegistrationOfSoldier");
                });

            modelBuilder.Entity("Aut3.Models.RequestsResponsesLog", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTimeOfChange")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdOfChangedItem")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NextValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviousValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhichModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhichValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhoChanged")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogId");

                    b.ToTable("RequestsResponsesLog");
                });

            modelBuilder.Entity("Aut3.Models.Soldier", b =>
                {
                    b.Property<Guid>("SoldierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MilitaryUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Pesel")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("PlaceOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.HasKey("SoldierId");

                    b.HasIndex("MilitaryUnitId");

                    b.ToTable("Soldier");
                });

            modelBuilder.Entity("Aut3.Models.Vehicle", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CarType")
                        .HasColumnType("int");

                    b.Property<Guid>("CurrUnitID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfProduction")
                        .HasColumnType("Date");

                    b.Property<int>("EngineCapacityCC")
                        .HasColumnType("int");

                    b.Property<int>("FuelConfig")
                        .HasColumnType("int");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PowerOutputHP")
                        .HasColumnType("int");

                    b.Property<Guid>("SoldierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TransmissionConfig")
                        .HasColumnType("int");

                    b.Property<string>("Vin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeightKg")
                        .HasColumnType("int");

                    b.HasKey("VehicleId");

                    b.HasIndex("CurrUnitID");

                    b.HasIndex("SoldierId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("Aut3.Models.Voivodeship", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Voivodeship");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Aut3.Models.City", b =>
                {
                    b.HasOne("Aut3.Models.County", "County")
                        .WithMany("Cities")
                        .HasForeignKey("CountyID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Aut3.Models.Voivodeship", "Voivodeship")
                        .WithMany()
                        .HasForeignKey("VoivodeshipID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("County");

                    b.Navigation("Voivodeship");
                });

            modelBuilder.Entity("Aut3.Models.County", b =>
                {
                    b.HasOne("Aut3.Models.Voivodeship", "Voivodeship")
                        .WithMany("Counties")
                        .HasForeignKey("VoivodeshipID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Voivodeship");
                });

            modelBuilder.Entity("Aut3.Models.FamilyRelationToSoldier", b =>
                {
                    b.HasOne("Aut3.Models.FamilyMember", "FamilyMember")
                        .WithMany("FamilyRelationToSoldiers")
                        .HasForeignKey("FamilyMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aut3.Models.Soldier", "Soldier")
                        .WithMany("FamilyRelationToSoldiers")
                        .HasForeignKey("SoldierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FamilyMember");

                    b.Navigation("Soldier");
                });

            modelBuilder.Entity("Aut3.Models.MilitaryUnit", b =>
                {
                    b.HasOne("Aut3.Models.County", "County")
                        .WithMany()
                        .HasForeignKey("CountyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("County");
                });

            modelBuilder.Entity("Aut3.Models.RegistrationOfSoldier", b =>
                {
                    b.HasOne("Aut3.Models.MilitaryUnit", "Unit")
                        .WithMany("RegistrationOfSoldiers")
                        .HasForeignKey("MilitaryUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aut3.Models.Soldier", "Soldier")
                        .WithOne("RegistrationOfSoldier")
                        .HasForeignKey("Aut3.Models.RegistrationOfSoldier", "SoldierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Soldier");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("Aut3.Models.Soldier", b =>
                {
                    b.HasOne("Aut3.Models.MilitaryUnit", "CurrUnit")
                        .WithMany("Soldiers")
                        .HasForeignKey("MilitaryUnitId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("CurrUnit");
                });

            modelBuilder.Entity("Aut3.Models.Vehicle", b =>
                {
                    b.HasOne("Aut3.Models.MilitaryUnit", "CurrUnit")
                        .WithMany("Vehicles")
                        .HasForeignKey("CurrUnitID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aut3.Models.Soldier", "Owner")
                        .WithMany("OwnedVehicles")
                        .HasForeignKey("SoldierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrUnit");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Aut3.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Aut3.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aut3.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Aut3.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aut3.Models.County", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Aut3.Models.FamilyMember", b =>
                {
                    b.Navigation("FamilyRelationToSoldiers");
                });

            modelBuilder.Entity("Aut3.Models.MilitaryUnit", b =>
                {
                    b.Navigation("RegistrationOfSoldiers");

                    b.Navigation("Soldiers");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Aut3.Models.Soldier", b =>
                {
                    b.Navigation("FamilyRelationToSoldiers");

                    b.Navigation("OwnedVehicles");

                    b.Navigation("RegistrationOfSoldier");
                });

            modelBuilder.Entity("Aut3.Models.Voivodeship", b =>
                {
                    b.Navigation("Counties");
                });
#pragma warning restore 612, 618
        }
    }
}
