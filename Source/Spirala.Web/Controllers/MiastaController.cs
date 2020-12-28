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
    public class MiastaController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public MiastaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Miasta
        [HttpGet]
        [ODataRoute("Miasta")]
        [Authorize(Roles = "Admin,User")]

        public IQueryable<Miasto> GetMiasta()
        {
            return  _context.Miasto;
        }
        [HttpPatch]
        [ODataRoute("Miasta")]
        [Authorize(Roles = "Admin,User")]

        public IActionResult  PatchMiasta([FromBody]  CityQuestion question)
        {
            
            var op = _context.Miasto.Any(a => a.Nazwa == question.Name);
            if (op){
                return Ok();
            }

            return NotFound();
        }


        // GET: api/Miasta/5
        [HttpGet("{id}")]
        [ODataRoute("Miasta/{id}")]
        public SingleResult<Miasto> GetCategory([FromODataUri] int id)
        {
            return SingleResult.Create(_context.Miasto.Where(c => c.ID == id));
        }

        // PUT: api/Miasta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ODataRoute("Miasta/{id}")]       
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutMiasto(int id, Miasto miasto)
        {
            if (id != miasto.ID)
            {
                return BadRequest();
            }

            _context.Entry(miasto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MiastoExists(id))
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

        // POST: api/Miasta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ODataRoute("Miasta")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Miasto>> PostMiasto(Miasto miasto)
        {
            _context.Miasto.Add(miasto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMiasta", new { id = miasto.ID }, miasto);
        }

        // DELETE: api/Miasta/5
        [HttpDelete("{id}")]
        [ODataRoute("Miasta/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteMiasto(int id)
        {
            var miasto = await _context.Miasto.FindAsync(id);
            if (miasto == null)
            {
                return NotFound();
            }

            _context.Miasto.Remove(miasto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MiastoExists(int id)
        {
            return _context.Miasto.Any(e => e.ID == id);
        }
    }
}
public class CityQuestion
{
   
    public string Name  { get; set; }
}