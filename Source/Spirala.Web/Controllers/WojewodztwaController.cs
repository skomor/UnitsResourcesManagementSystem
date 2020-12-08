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
    [ApiController]
    [Route("api/[controller]")]
    [EnableQuery]
    public class WojewodztwaController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public WojewodztwaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Wojewodztwo
        [HttpGet]
        [ODataRoute("Wojewodztwa")]

        public IQueryable<Wojewodztwo> GetWojewodztwa()
        {
            return  _context.Wojewodztwo;
        }

        // GET: api/Wojewodztwo/5
        [HttpGet("{id}")]
        [ODataRoute("Wojewodztwa/{id}")]
        public SingleResult<Wojewodztwo> GetCategory([FromODataUri] int id)
        {
            return SingleResult.Create(_context.Wojewodztwo.Where(c => c.ID == id));
        }

        // PUT: api/Wojewodztwo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ODataRoute("Wojewodztwa/{id}")]       
        public async Task<IActionResult> PutWojewodztwo(int id, Wojewodztwo wojewodztwo)
        {
            if (id != wojewodztwo.ID)
            {
                return BadRequest();
            }

            _context.Entry(wojewodztwo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WojewodztwoExists(id))
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

        // POST: api/Wojewodztwo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ODataRoute("Wojewodztwa")]
        public async Task<ActionResult<Wojewodztwo>> PostWojewodztwo(Wojewodztwo wojewodztwo)
        {
            _context.Wojewodztwo.Add(wojewodztwo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWojewodztwa", new { id = wojewodztwo.ID }, wojewodztwo);
        }

        // DELETE: api/Wojewodztwo/5
        [HttpDelete("{id}")]
        [ODataRoute("Wojewodztwa/{id}")]


        public async Task<IActionResult> DeleteWojewodztwo(int id)
        {
            var wojewodztwo = await _context.Wojewodztwo.FindAsync(id);
            if (wojewodztwo == null)
            {
                return NotFound();
            }

            _context.Wojewodztwo.Remove(wojewodztwo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WojewodztwoExists(int id)
        {
            return _context.Wojewodztwo.Any(e => e.ID == id);
        }
    }
}
