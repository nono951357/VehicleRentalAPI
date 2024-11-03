using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly VehicleRentalContext _context;

        public RentalsController(VehicleRentalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rental>> GetRentals()
        {
            return _context.Rentals
                           .Include(r => r.Customer)
                           .Include(r => r.Vehicle)
                           .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Rental> GetRental(int id)
        {
            var rental = _context.Rentals
                                 .Include(r => r.Customer)
                                 .Include(r => r.Vehicle)
                                 .FirstOrDefault(r => r.Id == id);

            if (rental == null)
            {
                return NotFound("A kölcsönzés nem található.");
            }

            return rental;
        }

        [HttpPost]
        public ActionResult<Rental> PostRental(Rental rental)
        {
            var vehicle = _context.Vehicles.Find(rental.VehicleId);

            if (vehicle == null || !vehicle.IsItAvalible)
            {
                return BadRequest("A jármű nem elérhető.");
            }

            rental.RentalDate = DateTime.Now;
            rental.ReturnDate = DateTime.Now.AddMonths(1);
            _context.Rentals.Add(rental);

            vehicle.IsItAvalible = false;
            _context.Entry(vehicle).State = EntityState.Modified;

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRental), new { id = rental.Id }, rental);
        }

        [HttpPut("{id}")]
        public IActionResult PutRental(int id, Rental rental)
        {
            if (id != rental.Id)
            {
                return BadRequest("Az azonosító nem egyezik.");
            }

            var vehicle = _context.Vehicles.Find(rental.VehicleId);

            if (vehicle == null || !vehicle.IsItAvalible)
            {
                return BadRequest("A jármű nem elérhető.");
            }

            _context.Entry(rental).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRental(int id)
        {
            var rental = _context.Rentals.Find(id);

            if (rental == null)
            {
                return NotFound("A kölcsönzés nem található.");
            }

            var vehicle = _context.Vehicles.Find(rental.VehicleId);

            if (vehicle != null)
            {
                // Jelöljük a járművet újra elérhetőként
                vehicle.IsItAvalible = true;
                _context.Entry(vehicle).State = EntityState.Modified;
            }

            _context.Rentals.Remove(rental);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
