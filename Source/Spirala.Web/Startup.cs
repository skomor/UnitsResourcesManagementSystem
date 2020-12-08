using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Aut3.Data;
using Aut3.Middleware;
using Aut3.Models;
using IdentityServer4.Services;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

namespace Aut3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));




            services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddControllers(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false);

            services.AddOData();


            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();


            services.AddTransient<IProfileService, ProfileService>();
            services.AddControllersWithViews();
            services.AddRazorPages();

       

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                    policy => policy.RequireRole("Admin"));
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(
                configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            
            app.UseRequestResponseLogging();
            
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();


            /*
            app.UseMvc(routeBuilder =>
            {
                /*
                routeBuilder.Select().Filter();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
                routeBuilder.Filter().OrderBy().Expand().Select().MaxTop(100);
                #1#

               // routeBuilder.EnableDependencyInjection();


            });
            */



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.Select().Filter();
                endpoints.MapODataRoute("odata", "odata", GetEdmModel());
                endpoints.Filter().OrderBy().Expand().Select().MaxTop(100).Count();

                /*endpoints.MapControllers();
                endpoints.EnableDependencyInjection();
                endpoints.Select().Filter().OrderBy().Count().MaxTop(10);*/
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
            CreateRoles(serviceProvider).Wait();
            
        }
        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<Soldier>("Soldiers");
            odataBuilder.EntitySet<FamilyMember>("FamilyMembers");
            odataBuilder.EntitySet<FamilyRelationToSoldier>("FamilyRelationToSoldiers");
            odataBuilder.EntitySet<MilitaryUnit>("MilitaryUnits");
            odataBuilder.EntitySet<Vehicle>("Vehicles");
            odataBuilder.EntitySet<RegistrationOfSoldier>("RegistrationOfSoldiers");
            odataBuilder.EntitySet<Wojewodztwo>("Wojewodztwa");
            odataBuilder.EntitySet<Powiat>("Powiaty");
            odataBuilder.EntitySet<Miasto>("Miasta");

            return odataBuilder.GetEdmModel();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = {"Admin", "User"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                bool roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            ApplicationUser adminUser = new ApplicationUser
            {
                UserName = "skomorowski2@gmail.pl",
                Email = "skomorowski2@gmail.pl"
            };

            string adminPass = "Qwerty123!";
            ApplicationUser _user = await UserManager.FindByEmailAsync("skomorowski2@gmail.pl");

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(adminUser, adminPass);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}