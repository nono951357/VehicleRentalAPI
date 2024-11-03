using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleRentalContext _context;

        public VehiclesController(VehicleRentalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetVehicles()
        {
            return _context.Vehicles.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetVehicle(int id)
        {
            var vehicle = _context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        [HttpPost]
        public ActionResult<Vehicle> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }

        [HttpPut("{id}")]
        public IActionResult PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = _context.Vehicles.Find(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
