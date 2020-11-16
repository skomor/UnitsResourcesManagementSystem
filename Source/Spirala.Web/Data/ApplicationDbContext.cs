using Aut3.Models;
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

     
        public static IQueryable<object> Set ( DbContext _context, Type t)
        {
            return (IQueryable<object>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }
        public DbSet<Aut3.Models.Soldier> Soldier { get; set; }

        public DbSet<Aut3.Models.FamilyMember> FamilyMember { get; set; }

        public DbSet<Aut3.Models.FamilyRelationToSoldier> FamilyRelationToSoldier { get; set; }

        public DbSet<Aut3.Models.MilitaryUnit> MilitaryUnit { get; set; }

        public DbSet<Aut3.Models.RegistrationOfSoldier> RegistrationOfSoldier { get; set; }

        public DbSet<Aut3.Models.Vehicle> Vehicle { get; set; }
        
        public DbSet<Aut3.Models.RequestsResponsesLog> RequestsResponsesLog { get; set; }
    }
}