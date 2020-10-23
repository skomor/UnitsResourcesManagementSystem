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
using Microsoft.AspNetCore.Authorization;

namespace Aut3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoldiersController : ControllerBase
    {


        private readonly ApplicationDbContext _context;

        public SoldiersController(ApplicationDbContext context)
        {

            _context = context;
        }

        // GET: api/Soldiers
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Soldier>>> GetSoldier()
        {
            return await _context.Soldier.ToListAsync();
        }

        /*// GET: api/Soldiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Soldier>> GetSoldier(Guid id)
        {
            var soldier = await _context.Soldier.FindAsync(id);

            if (soldier == null)
            {
                return NotFound();
            }

            return soldier;
        }

        // PUT: api/Soldiers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoldier(Guid id, Soldier soldier)
        {
            if (id != soldier.Id)
            {
                return BadRequest();
            }

            _context.Entry(soldier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoldierExists(id))
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

        // POST: api/Soldiers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Soldier>> PostSoldier(Soldier soldier)
        {
            _context.Soldier.Add(soldier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSoldier", new { id = soldier.Id }, soldier);
        }

        // DELETE: api/Soldiers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Soldier>> DeleteSoldier(Guid id)
        {
            var soldier = await _context.Soldier.FindAsync(id);
            if (soldier == null)
            {
                return NotFound();
            }

            _context.Soldier.Remove(soldier);
            await _context.SaveChangesAsync();

            return soldier;
        }

        private bool SoldierExists(Guid id)
        {
            return _context.Soldier.Any(e => e.Id == id);
        }*/
    }
}
