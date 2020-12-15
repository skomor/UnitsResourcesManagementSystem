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
    public class PowiatyController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public PowiatyController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        // GET: api/Powiaty
        [HttpGet]
        [ODataRoute("Powiaty")]

        public IQueryable<Powiat> GetPowiat()
        {
            return  _context.Powiat;
        }

        // GET: api/Powiaty/5
        [HttpGet("{id}")]
        [ODataRoute("Powiaty/{id}")]

        public SingleResult<Powiat> GetCategory([FromODataUri] int id)
        {
            return SingleResult.Create(_context.Powiat.Where(c => c.ID == id));
        }

        // PUT: api/Powiaty/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ODataRoute("Powiaty/{id}")]       
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutPowiat(int id, Powiat powiat)
        {
            if (id != powiat.ID)
            {
                return BadRequest();
            }

            _context.Entry(powiat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PowiatExists(id))
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

        // POST: api/Powiaty
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ODataRoute("Powiaty")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Powiat>> PostPowiat(Powiat powiat)
        {
            _context.Powiat.Add(powiat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPowiat", new { id = powiat.ID }, powiat);
        }

        // DELETE: api/Powiaty/5
        [HttpDelete("{id}")]
        [ODataRoute("Powiaty/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeletePowiat(int id)
        {
            var powiat = await _context.Powiat.FindAsync(id);
            if (powiat == null)
            {
                return NotFound();
            }

            _context.Powiat.Remove(powiat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PowiatExists(int id)
        {
            return _context.Powiat.Any(e => e.ID == id);
        }
    }
}
