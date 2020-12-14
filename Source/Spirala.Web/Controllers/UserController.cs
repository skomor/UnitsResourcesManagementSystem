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
        private ApplicationDbContext _context;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> role, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = role;
            this._context = context;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            this.Users = userManager.Users.Include(x=> x.Unit);

            return this.Users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] putBody pb)
        {
            var newRole = pb.newRole;
            var newUnitId = pb.newUnitId;
            
            var user = await userManager.FindByIdAsync(id);

            var rolesInUser = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, rolesInUser.ToArray());


            if (user != null){
                if (_context.MilitaryUnit.Where(x => x.MilitaryUnitId == newUnitId).Any()){
                    user.Unit = await _context.MilitaryUnit.FindAsync(newUnitId);
                }else{
                    return BadRequest();

                }
             

                // var roles = roleManager.Roles.ToList();
                IdentityRole newR = new IdentityRole(newRole);

                if (await roleManager.RoleExistsAsync(newR.Name)){
                    var res2 = await userManager.AddToRoleAsync(user, newRole);
                    if (res2.Succeeded)
                        return NoContent();
                    else{
                        return BadRequest();
                    }
                }
            }

            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }

        public class putBody
        {
            public string newRole { get; set; }
            public Guid  newUnitId { get; set; }
        }
    }
}