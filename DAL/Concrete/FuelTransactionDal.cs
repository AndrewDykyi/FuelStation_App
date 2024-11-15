using Dapper;
using Npgsql;

public class FuelTransactionDal : IFuelTransactionDal
{
    private readonly string _connectionString;

    public FuelTransactionDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<FuelTransactionDto>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM fuel_transactions";
        return await connection.QueryAsync<FuelTransactionDto>(sql);
    }

    public async Task<FuelTransactionDto> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM fuel_transactions WHERE transaction_id = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<FuelTransactionDto>(sql, new { Id = id });

        return result ?? new FuelTransactionDto();
    }

    public async Task InsertAsync(FuelTransactionDto transaction)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "INSERT INTO fuel_transactions (customer_id, vehicle_id, pump_id, station_id, date_time, amount, total_price) " +
                  "VALUES (@CustomerId, @VehicleId, @PumpId, @StationId, @DateTime, @Amount, @TotalPrice)";
        await connection.ExecuteAsync(sql, transaction);
    }

    public async Task UpdateAsync(FuelTransactionDto transaction)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "UPDATE fuel_transactions SET customer_id = @CustomerId, vehicle_id = @VehicleId, pump_id = @PumpId, " +
                  "station_id = @StationId, date_time = @DateTime, amount = @Amount, total_price = @TotalPrice " +
                  "WHERE transaction_id = @TransactionId";
        await connection.ExecuteAsync(sql, transaction);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "DELETE FROM fuel_transactions WHERE transaction_id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
