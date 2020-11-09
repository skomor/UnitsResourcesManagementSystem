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

namespace Aut3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableQuery]

    public class FamilyRelationToSoldiersController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public FamilyRelationToSoldiersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FamilyRelationToSoldiers
        [HttpGet]
        [ODataRoute]
        public async Task<ActionResult<IEnumerable<FamilyRelationToSoldier>>> Get()
        {
            return await _context.FamilyRelationToSoldier.ToListAsync();
        }

        // GET: api/FamilyRelationToSoldiers/5
        [HttpGet("{id}")]
        [ODataRoute("FamilyRelationToSoldiers/{id}")]

        public async Task<ActionResult<FamilyRelationToSoldier>> Get(int id)
        {
            var familyRelationToSoldier = await _context.FamilyRelationToSoldier.FindAsync(id);

            if (familyRelationToSoldier == null)
            {
                return NotFound();
            }

            return familyRelationToSoldier;
        }

        // PUT: api/FamilyRelationToSoldiers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ODataRoute("FamilyRelationToSoldiers/{id}")]

        public async Task<IActionResult> PutFamilyRelationToSoldier(int id, FamilyRelationToSoldier familyRelationToSoldier)
        {
            if (id != familyRelationToSoldier.FamilyRelationToSoldierId)
            {
                return BadRequest();
            }

            _context.Entry(familyRelationToSoldier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyRelationToSoldierExists(id))
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

        // POST: api/FamilyRelationToSoldiers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ODataRoute("FamilyRelationToSoldiers")]

        public async Task<ActionResult<FamilyRelationToSoldier>> PostFamilyRelationToSoldier(FamilyRelationToSoldier familyRelationToSoldier)
        {
            _context.FamilyRelationToSoldier.Add(familyRelationToSoldier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = familyRelationToSoldier.FamilyRelationToSoldierId }, familyRelationToSoldier);
        }

        // DELETE: api/FamilyRelationToSoldiers/5
        [HttpDelete("{id}")]
        [ODataRoute("FamilyRelationToSoldiers/{id}")]

        public async Task<ActionResult<FamilyRelationToSoldier>> DeleteFamilyRelationToSoldier(int id)
        {
            var familyRelationToSoldier = await _context.FamilyRelationToSoldier.FindAsync(id);
            if (familyRelationToSoldier == null)
            {
                return NotFound();
            }

            _context.FamilyRelationToSoldier.Remove(familyRelationToSoldier);
            await _context.SaveChangesAsync();

            return familyRelationToSoldier;
        }

        private bool FamilyRelationToSoldierExists(int id)
        {
            return _context.FamilyRelationToSoldier.Any(e => e.FamilyRelationToSoldierId == id);
        }
    }
}
