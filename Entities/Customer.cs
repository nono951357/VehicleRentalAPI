using System.ComponentModel.DataAnnotations;

namespace VehicleRentalAPI.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }
    }
}
