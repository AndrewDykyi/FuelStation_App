public class FuelTransactionDto
{
    public int TransactionId { get; set; }
    public int CustomerId { get; set; }
    public int VehicleId { get; set; }
    public int PumpId { get; set; }
    public int StationId { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Amount { get; set; }
    public decimal TotalPrice { get; set; }
}