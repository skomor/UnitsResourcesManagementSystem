using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aut3.Data;
using Aut3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aut3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public IEnumerable<ApplicationUser> Users { get; set; }

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
     //   private readonly SignInManager<ApplicationDbContext> _signInManager;
        private ApplicationDbContext _context;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> role, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = role;
            this._context = context;
        //    this._signInManager = manager;
        }

        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IEnumerable<ApplicationUser> Get()
        {
            this.Users = userManager.Users;

            return this.Users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IEnumerable<string>> Get(string id)
        {
            IEnumerable<string> empty = new String[0];

            var tempUser = await userManager.FindByIdAsync(id);

            if (tempUser != null){
                var role = await userManager.GetRolesAsync(tempUser);

                return role;
            }
            else return empty;
        }

        // POST api/<UserController>
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Put(string id, [FromBody] string[] roles)
        {
            //var newRole = pb.newRole;
            
            var user = await userManager.FindByIdAsync(id);

            var rolesInUser = await userManager.GetRolesAsync(user);
            
            await userManager.RemoveFromRolesAsync(user, rolesInUser.ToArray());


            if (user != null){
                /*if (_context.MilitaryUnit.Where(x => x.MilitaryUnitId == newUnitId).Any()){
                    user.Unit = await _context.MilitaryUnit.FindAsync(newUnitId);
                }else{
                    return BadRequest();

                }*/
             

                // var roles = roleManager.Roles.ToList();

                bool doRolesExist = true;
                foreach (var role in roles){
                   doRolesExist =  doRolesExist && await roleManager.RoleExistsAsync(new IdentityRole(role).Name);
                }

                if (doRolesExist){
                    IdentityResult res2 = IdentityResult.Failed();
                    /*if (rolesInUser.Contains(newRole)){
                         res2 =  await userManager.AddToRolesAsync(user, rolesInUser);

                    }
                    else{*/
                    res2 = await userManager.AddToRolesAsync(user, roles);

                    // }

                    if (res2.Succeeded){
                    

                   // await _signInManager.RefreshSignInAsync(_context);
                    return NoContent();
                }
                else{
                        return BadRequest();
                    }
                }
            }

            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public void Delete(string id)
        {
        }

        public class putBody
        {
            public string newRole { get; set; }
        }
    }
}