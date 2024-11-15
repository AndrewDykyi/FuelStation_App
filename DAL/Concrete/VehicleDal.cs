using Npgsql;
using Dapper;

public class VehicleDal : IVehicleDal
{
    private readonly string _connectionString;

    public VehicleDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<VehicleDto>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT vehicle_id AS VehicleId, customer_id, license_plate AS LicensePlate, model, fuel_type FROM vehicles";
        return await connection.QueryAsync<VehicleDto>(sql);
    }

    public async Task<VehicleDto> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM vehicles WHERE vehicle_id = @Id";

        return await connection.QueryFirstOrDefaultAsync<VehicleDto>(sql, new { Id = id });
    }
    public async Task DeleteByLicensePlateAsync(string licensePlate)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync("DELETE FROM vehicles WHERE license_plate = @LicensePlate", new { LicensePlate = licensePlate });
    }


    public async Task<int> InsertAsync(VehicleDto vehicle)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "INSERT INTO vehicles (customer_id, license_plate, model, fuel_type) VALUES (@CustomerId, @LicensePlate, @Model, @FuelType) RETURNING vehicle_id";
        var vehicleId = await connection.ExecuteScalarAsync<int>(sql, vehicle);
        return vehicleId;
    }

    public async Task UpdateAsync(VehicleDto vehicle)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "UPDATE vehicles SET customer_id = @CustomerId, license_plate = @LicensePlate, model = @Model, fuel_type = @FuelType WHERE vehicle_id = @VehicleId";
        await connection.ExecuteAsync(sql, vehicle);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "DELETE FROM vehicles WHERE vehicle_id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
