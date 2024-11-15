using Dapper;
using Npgsql;

public class FuelPumpDal : IFuelPumpDal
{
    private readonly string _connectionString;

    public FuelPumpDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<FuelPumpDto>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM fuel_pumps";
        return await connection.QueryAsync<FuelPumpDto>(sql);
    }

    public async Task<FuelPumpDto> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM fuel_pumps WHERE pump_id = @Id";
        return await connection.QueryFirstOrDefaultAsync<FuelPumpDto>(sql, new { Id = id });
    }

    public async Task InsertAsync(FuelPumpDto pump)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "INSERT INTO fuel_pumps (fuel_type, location, station_id) VALUES (@FuelType, @Location, @StationId)";
        await connection.ExecuteAsync(sql, pump);
    }

    public async Task UpdateAsync(FuelPumpDto pump)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "UPDATE fuel_pumps SET fuel_type = @FuelType, location = @Location, station_id = @StationId WHERE pump_id = @PumpId";
        await connection.ExecuteAsync(sql, pump);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "DELETE FROM fuel_pumps WHERE pump_id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}