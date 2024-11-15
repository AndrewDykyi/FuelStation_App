using Dapper;
using Npgsql;

public class FuelStationDal : IFuelStationDal
{
    private readonly string _connectionString;

    public FuelStationDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<FuelStationDto>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT station_id AS FuelStationId, name, location FROM fuel_stations";
        return await connection.QueryAsync<FuelStationDto>(sql);
    }

    public async Task<FuelStationDto> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM fuel_stations WHERE station_id = @Id";
        return await connection.QueryFirstOrDefaultAsync<FuelStationDto>(sql, new { Id = id });
    }

    public async Task InsertAsync(FuelStationDto station)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "INSERT INTO fuel_stations (name, location, manager_name) VALUES (@Name, @Location, @ManagerName)";
        await connection.ExecuteAsync(sql, station);
    }

    public async Task UpdateAsync(FuelStationDto station)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "UPDATE fuel_stations SET name = @Name, location = @Location, manager_name = @ManagerName WHERE station_id = @StationId";
        await connection.ExecuteAsync(sql, station);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "DELETE FROM fuel_stations WHERE station_id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}