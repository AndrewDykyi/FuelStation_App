using Dapper;
using Npgsql;

public class EmployeeDal : IEmployeeDal
{
    private readonly string _connectionString;

    public EmployeeDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM employee";
        return await connection.QueryAsync<EmployeeDto>(sql);
    }

    public async Task<EmployeeDto> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM employee WHERE employee_id = @Id";
        return await connection.QueryFirstOrDefaultAsync<EmployeeDto>(sql, new { Id = id });
    }

    public async Task InsertAsync(EmployeeDto employee)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "INSERT INTO employee (name, position, station_id, hire_date) VALUES (@Name, @Position, @StationId, @HireDate)";
        await connection.ExecuteAsync(sql, employee);
    }

    public async Task UpdateAsync(EmployeeDto employee)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "UPDATE employee SET name = @Name, position = @Position, station_id = @StationId, hire_date = @HireDate WHERE employee_id = @EmployeeId";
        await connection.ExecuteAsync(sql, employee);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "DELETE FROM employee WHERE employee_id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}