using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aut3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aut3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
     
        public IEnumerable<ApplicationUser> Users { get; set; }

        RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> role)
        {
            this.userManager = userManager;
            this.roleManager = role;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            this.Users = userManager.Users;

            return this.Users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<string>> Get(string id)
        {
            IEnumerable<string> empty = new String[0];

            var tempUser = await userManager.FindByIdAsync(id);

            if (tempUser != null)
            {
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
        public async Task<IActionResult> Put(string id, [FromBody] string newRole)
        {
            var user = await userManager.FindByIdAsync(id);

            var rolesInUser = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, rolesInUser.ToArray());

            if (user != null)
            {
                var roles = roleManager.Roles.ToList();
                IdentityRole newR = new IdentityRole(newRole);
                if (await roleManager.RoleExistsAsync(newR.Name))
                {
                    var res2 = await userManager.AddToRoleAsync(user, newRole);
                    if (res2.Succeeded)
                        return NoContent();
                    else
                    {
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
    }
}