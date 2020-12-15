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
    [Route("api/[controller]")]
    [ApiController]
    [EnableQuery]

    public class MilitaryUnitsController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public MilitaryUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MilitaryUnits
        [HttpGet]
        [ODataRoute("MilitaryUnits")]

        public  IQueryable<MilitaryUnit> GetMilitaryUnit()
        {
            return  _context.MilitaryUnit;
        }

        // GET: api/MilitaryUnits/5
        [HttpGet("{id}")]
        [ODataRoute("MilitaryUnits/{id}")]

        public SingleResult<MilitaryUnit> GetCategory([FromODataUri] Guid id)
        {
            return SingleResult.Create(_context.MilitaryUnit.Where(c => c.MilitaryUnitId == id));
        }
        
    

        // PUT: api/MilitaryUnits/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ODataRoute("MilitaryUnits/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutMilitaryUnit(Guid id, MilitaryUnit militaryUnit)
        {
            if (id != militaryUnit.MilitaryUnitId)
            {
                return BadRequest();
            }

            _context.Entry(militaryUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MilitaryUnitExists(id))
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

        // POST: api/MilitaryUnits
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ODataRoute("MilitaryUnits")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<MilitaryUnit>> PostMilitaryUnit(MilitaryUnit militaryUnit)
        {
            _context.MilitaryUnit.Add(militaryUnit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMilitaryUnit", new { id = militaryUnit.MilitaryUnitId }, militaryUnit);
        }

        // DELETE: api/MilitaryUnits/5
        [HttpDelete("{id}")]
        [ODataRoute("MilitaryUnits/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<MilitaryUnit>> DeleteMilitaryUnit(Guid id)
        {
            var militaryUnit = await _context.MilitaryUnit.FindAsync(id);
            if (militaryUnit == null)
            {
                return NotFound();
            }

            _context.MilitaryUnit.Remove(militaryUnit);
            await _context.SaveChangesAsync();

            return militaryUnit;
        }

        private bool MilitaryUnitExists(Guid id)
        {
            return _context.MilitaryUnit.Any(e => e.MilitaryUnitId == id);
        }
    }
}
