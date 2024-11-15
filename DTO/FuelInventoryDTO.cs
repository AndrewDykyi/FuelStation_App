public class FuelInventoryDTO
{
    public int InventoryId { get; set; }
    public int StationId { get; set; }
    public string? FuelType { get; set; }
    public double Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
}
