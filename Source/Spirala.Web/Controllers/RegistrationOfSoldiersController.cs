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

    public class RegistrationOfSoldiersController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public RegistrationOfSoldiersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RegistrationOfSoldiers
        [HttpGet]
        [ODataRoute("RegistrationOfSoldiers")]
        public  IQueryable<RegistrationOfSoldier> GetRegistrationOfSoldier()
        {
            return  _context.RegistrationOfSoldier;
        }


        

        // GET: api/RegistrationOfSoldiers/5
        [HttpGet("{id}")]
        [ODataRoute("RegistrationOfSoldiers/{id}")]
        
        public SingleResult<RegistrationOfSoldier> GetCategory([FromODataUri] Guid id)
        {
            return SingleResult.Create(_context.RegistrationOfSoldier.Where(c => c.RegistrationOfSoldierId == id));
        }

        

        // PUT: api/RegistrationOfSoldiers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ODataRoute("RegistrationOfSoldiers/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutRegistrationOfSoldier(Guid id, RegistrationOfSoldier registrationOfSoldier)
        {
            if (id != registrationOfSoldier.RegistrationOfSoldierId)
            {
                return BadRequest();
            }

            _context.Entry(registrationOfSoldier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationOfSoldierExists(id))
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

        // POST: api/RegistrationOfSoldiers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ODataRoute("RegistrationOfSoldiers")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<RegistrationOfSoldier>> PostRegistrationOfSoldier(RegistrationOfSoldier registrationOfSoldier)
        {
            _context.RegistrationOfSoldier.Add(registrationOfSoldier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistrationOfSoldier", new { id = registrationOfSoldier.RegistrationOfSoldierId }, registrationOfSoldier);
        }

        // DELETE: api/RegistrationOfSoldiers/5
        [HttpDelete("{id}")]
        [ODataRoute("RegistrationOfSoldiers/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<RegistrationOfSoldier>> DeleteRegistrationOfSoldier(Guid id)
        {
            var registrationOfSoldier = await _context.RegistrationOfSoldier.FindAsync(id);
            if (registrationOfSoldier == null)
            {
                return NotFound();
            }

            _context.RegistrationOfSoldier.Remove(registrationOfSoldier);
            await _context.SaveChangesAsync();

            return registrationOfSoldier;
        }

        private bool RegistrationOfSoldierExists(Guid id)
        {
            return _context.RegistrationOfSoldier.Any(e => e.RegistrationOfSoldierId == id);
        }
    }
}
