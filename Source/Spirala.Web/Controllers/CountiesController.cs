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
    public class CountiesController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public CountiesController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        // GET: api/Counties
        [HttpGet]
        [ODataRoute("Counties")]
        [Authorize(Roles = "Admin,User")]

        public IQueryable<County> GetCounty()
        {
            return  _context.County;
        }

        // GET: api/Counties/5
        [HttpGet("{id}")]
        [ODataRoute("Counties/{id}")]
        [Authorize(Roles = "Admin,User")]

        public SingleResult<County> GetCategory([FromODataUri] int id)
        {
            return SingleResult.Create(_context.County.Where(c => c.ID == id));
        }

        // PUT: api/Counties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ODataRoute("Counties/{id}")]       
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutCounty(int id, County county)
        {
            if (id != county.ID)
            {
                return BadRequest();
            }

            _context.Entry(county).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountyExists(id))
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

        // POST: api/Counties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ODataRoute("Counties")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<County>> PostCounty(County county)
        {
            _context.County.Add(county);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCounty", new { id = county.ID }, county);
        }

        // DELETE: api/Counties/5
        [HttpDelete("{id}")]
        [ODataRoute("Counties/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteCounty(int id)
        {
            var county = await _context.County.FindAsync(id);
            if (county == null)
            {
                return NotFound();
            }

            _context.County.Remove(county);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountyExists(int id)
        {
            return _context.County.Any(e => e.ID == id);
        }
    }
}
