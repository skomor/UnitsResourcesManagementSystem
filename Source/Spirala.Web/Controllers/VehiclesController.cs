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

    public class VehiclesController : ODataController
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        [ODataRoute("Vehicles")]
        [Authorize(Roles = "Admin,User")]

        public IQueryable<Vehicle> GetVehicle()
        {
            return  _context.Vehicle;
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        [ODataRoute("Vehicles/{id}")]
        [Authorize(Roles = "Admin,User")]


        public SingleResult<Vehicle> GetCategory([FromODataUri] Guid id)
        {
            return SingleResult.Create(_context.Vehicle.Where(c => c.VehicleId == id));
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ODataRoute("Vehicles/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutVehicle(Guid id, Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ODataRoute("Vehicles")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicle.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        [ODataRoute("Vehicles/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Vehicle>> DeleteVehicle(Guid id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        private bool VehicleExists(Guid id)
        {
            return _context.Vehicle.Any(e => e.VehicleId == id);
        }
    }
}
