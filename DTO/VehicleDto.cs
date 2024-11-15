public class VehicleDto
{
    public int VehicleId { get; set; }
    public int CustomerId { get; set; }
    public string? LicensePlate { get; set; }
    public string? Model { get; set; }
    public string? FuelType { get; set; }
    public DateTime CreatedAt { get; set; }
}