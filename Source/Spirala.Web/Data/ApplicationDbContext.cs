﻿using Aut3.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Aut3.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
                : base(options, operationalStoreOptions)
        {
        }

     
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
            modelBuilder.Entity<MilitaryUnit>()
                .HasIndex(u => u.Name)
                .IsUnique();
            modelBuilder.Entity<MilitaryUnit>()
                .HasIndex(u => u.UnitNumber)
                .IsUnique();

            
            modelBuilder.Entity<MilitaryUnit>()
                .HasMany(c => c.Soldiers)
                .WithOne(e => e.CurrUnit).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Soldier>()
                .HasOne(c => c.CurrUnit)
                .WithMany(e => e.Soldiers).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            
            
            modelBuilder.Entity<City>()
                .HasOne(c => c.County).WithMany(e => e.Cities).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<County>()
                .HasOne(c => c.Voivodeship).WithMany(e => e.Counties).OnDelete(DeleteBehavior.NoAction);
       


        }  
       


        public DbSet<Aut3.Models.Soldier> Soldier { get; set; }
        

        public DbSet<Aut3.Models.FamilyMember> FamilyMember { get; set; }

        public DbSet<Aut3.Models.FamilyRelationToSoldier> FamilyRelationToSoldier { get; set; }

        public DbSet<Aut3.Models.MilitaryUnit> MilitaryUnit { get; set; }

        public DbSet<Aut3.Models.RegistrationOfSoldier> RegistrationOfSoldier { get; set; }

        public DbSet<Aut3.Models.Vehicle> Vehicle { get; set; }
        public DbSet<Aut3.Models.Voivodeship> Voivodeship { get; set; }
        public DbSet<Aut3.Models.County> County { get; set; }
        public DbSet<Aut3.Models.City> City { get; set; }
        
        public DbSet<Aut3.Models.RequestsResponsesLog> RequestsResponsesLog { get; set; }
    }
}