using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VehicleRentalAPI.Entities;

public class Rental
{
    public int Id { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int VehicleId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }

    // Navigation properties
    [JsonIgnore]
    public required Customer Customer { get; set; }
    [JsonIgnore]
    public required Vehicle Vehicle { get; set; }
}
