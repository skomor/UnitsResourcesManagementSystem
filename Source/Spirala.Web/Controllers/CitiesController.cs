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
    public class CitiesController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        [ODataRoute("Cities")]
        [Authorize(Roles = "Admin,User")]

        public IQueryable<City> GetCities()
        {
            return  _context.City;
        }
        [HttpPatch]
        [ODataRoute("Cities")]
        [Authorize(Roles = "Admin,User")]

        public IActionResult  PatchCities([FromBody]  CityQuestion question)
        {
            
            var op = _context.City.Any(a => a.Name == question.Name);
            if (op){
                return Ok();
            }

            return NotFound();
        }


        // GET: api/Cities/5
        [HttpGet("{id}")]
        [ODataRoute("Cities/{id}")]
        public SingleResult<City> GetCategory([FromODataUri] int id)
        {
            return SingleResult.Create(_context.City.Where(c => c.ID == id));
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ODataRoute("Cities/{id}")]       
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.ID)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ODataRoute("Cities")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<City>> PostCity(City city)
        {
            _context.City.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCities", new { id = city.ID }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        [ODataRoute("Cities/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.ID == id);
        }
    }
}
public class CityQuestion
{
    public string Name  { get; set; }
}