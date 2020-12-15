using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aut3.Data;
using Aut3.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;

namespace Aut3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableQuery]

    public class FamilyMembersController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public FamilyMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FamilyMembers
        [HttpGet]
        [ODataRoute("FamilyMembers")]

        public  IQueryable<FamilyMember> GetFamilyMember()
        {
            return  _context.FamilyMember;
        }

        // GET: api/FamilyMembers/5
        [HttpGet("{id}")]
        [ODataRoute("FamilyMembers/{id}")]
        public SingleResult<FamilyMember> GetCategory([FromODataUri] Guid id)
        {
            return SingleResult.Create(_context.FamilyMember.Where(c => c.FamilyMemberId == id));
        }
        /*public async Task<ActionResult<FamilyMember>> GetFamilyMember([FromODataUri] Guid id)
        {
            var familyMember = await _context.FamilyMember.FindAsync(id);

            if (familyMember == null)
            {
                return NotFound();
            }

            return familyMember;
        }*/

        // PUT: api/FamilyMembers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ODataRoute("FamilyMembers/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutFamilyMember(Guid id, FamilyMember familyMember)
        {
            if (id != familyMember.FamilyMemberId)
            {
                return BadRequest();
            }

            _context.Entry(familyMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyMemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FamilyMembers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ODataRoute("FamilyMembers")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<FamilyMember>> PostFamilyMember(FamilyMember familyMember)
        {
            _context.FamilyMember.Add(familyMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFamilyMember", new { id = familyMember.FamilyMemberId }, familyMember);
        }

        // DELETE: api/FamilyMembers/5
        [HttpDelete("{id}")]
        [ODataRoute("FamilyMembers/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<FamilyMember>> DeleteFamilyMember(Guid id)
        {
            var familyMember = await _context.FamilyMember.FindAsync(id);
            if (familyMember == null)
            {
                return NotFound();
            }

            _context.FamilyMember.Remove(familyMember);
            await _context.SaveChangesAsync();

            return familyMember;
        }

        private bool FamilyMemberExists(Guid id)
        {
            return _context.FamilyMember.Any(e => e.FamilyMemberId == id);
        }
    }
}
