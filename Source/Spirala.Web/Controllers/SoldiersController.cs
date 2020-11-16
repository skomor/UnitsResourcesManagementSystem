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
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Aut3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableQuery]


    public class SoldiersController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public SoldiersController(ApplicationDbContext context)
        {
            _context = context;
        
        }

        // GET: api/Soldier
        [HttpGet]
        [ODataRoute("Soldiers")]

        // public  Task<ActionResult<IEnumerable<Soldier>>> GetSoldier()
        public async Task<IEnumerable<Soldier>> GetSoldier()
        {
            return await _context.Soldier.ToListAsync();
            
          
        }

        // GET: api/Soldiers/5
        [HttpGet("{id}")]
        [ODataRoute("Soldiers/{id}")]
        public async Task<ActionResult<Soldier>> GetSoldier([FromODataUri] Guid id)
        {
            var soldier = await _context.Soldier.FindAsync(id);

            if (soldier == null)
            {
                return NotFound();
            }

            return soldier;
        }

        // PUT: api/Soldiers/5

        [HttpPut("{id}")]
        [ODataRoute("Soldiers/{id}")]
        public async Task<IActionResult> PutSoldier(Guid id, Soldier soldier)
        {
            if (id != soldier.SoldierId)
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
        
        [ODataRoute("Soldiers")]
        public async Task<ActionResult<Soldier>> PostSoldier( Soldier soldier)
        {
            await _context.Soldier.AddAsync(soldier);
            await _context.SaveChangesAsync();

           // return CreatedAtAction("GetSoldier", new {id = soldier.Id}, soldier); //TODO: Wyrzuca dziwny error znowu związany z routingiem zastąpiono gorszym rozwiązaniem 
           return Ok(await _context.Soldier.FindAsync( soldier.SoldierId));
        }

        // DELETE: api/Soldiers/5
        [HttpDelete("{id}")]
        [ODataRoute("Soldiers/{id}")]

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
            return _context.Soldier.Any(e => e.SoldierId == id);
        }
    }
}