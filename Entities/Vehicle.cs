using System.ComponentModel.DataAnnotations;

namespace VehicleRentalAPI.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        public required string Model { get; set; }
        [Required]
        public required string LicensePlate { get; set; }
        [Required]
        public decimal DailyRate { get; set; }
        [Required]
        public bool IsItAvalible { get; set; }
    }
}
