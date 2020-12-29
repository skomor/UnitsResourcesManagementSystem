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
    public class VoivodeshipsController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public VoivodeshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Voivodeship
        [HttpGet]
        [ODataRoute("Voivodeships")]
        [Authorize(Roles = "Admin,User")]

        public IQueryable<Voivodeship> GetVoivodeships()
        {
            return  _context.Voivodeship;
        }

        // GET: api/Voivodeship/5
        [HttpGet("{id}")]
        [ODataRoute("Voivodeships/{id}")]
        [Authorize(Roles = "Admin,User")]

        public SingleResult<Voivodeship> GetCategory([FromODataUri] int id)
        {
            return SingleResult.Create(_context.Voivodeship.Where(c => c.ID == id));
        }

        // PUT: api/Voivodeship/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ODataRoute("Voivodeships/{id}")]       
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutVoivodeship(int id, Voivodeship voivodeship)
        {
            if (id != voivodeship.ID)
            {
                return BadRequest();
            }

            _context.Entry(voivodeship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoivodeshipExists(id))
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

        // POST: api/Voivodeship
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ODataRoute("Voivodeships")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Voivodeship>> PostVoivodeship(Voivodeship voivodeship)
        {
            _context.Voivodeship.Add(voivodeship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoivodeships", new { id = voivodeship.ID }, voivodeship);
        }

        // DELETE: api/Voivodeship/5
        [HttpDelete("{id}")]
        [ODataRoute("Voivodeships/{id}")]
        [Authorize(Roles = "Admin")]



        public async Task<IActionResult> DeleteVoivodeship(int id)
        {
            var voivodeship = await _context.Voivodeship.FindAsync(id);
            if (voivodeship == null)
            {
                return NotFound();
            }

            _context.Voivodeship.Remove(voivodeship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoivodeshipExists(int id)
        {
            return _context.Voivodeship.Any(e => e.ID == id);
        }
    }
}
