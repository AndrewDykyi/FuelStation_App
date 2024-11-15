using Npgsql;
using Dapper;

public class CustomerDal : ICustomerDal
{
    private readonly string _connectionString;

    public CustomerDal(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT customer_id AS CustomerId, name, email FROM customers";
        return await connection.QueryAsync<CustomerDto>(sql);
    }

    public async Task<CustomerDto?> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var customer = await connection.QueryFirstOrDefaultAsync<CustomerDto>("SELECT * FROM customers WHERE customer_id = @Id", new { Id = id });

        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {id} not found.");
        }

        return customer;
    }


    public async Task<int> InsertAsync(CustomerDto customer)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "INSERT INTO customers (name, email) VALUES (@Name, @Email) RETURNING customer_id";
        var customerId = await connection.ExecuteScalarAsync<int>(sql, customer);
        return customerId;
    }

    public async Task UpdateAsync(CustomerDto customer)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "UPDATE customers SET name = @Name, email = @Email WHERE customer_id = @CustomerId";
        await connection.ExecuteAsync(sql, customer);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync("DELETE FROM customers WHERE customer_id = @Id", new { Id = id });
    }
    public async Task<CustomerDto?> GetByNameAsync(string name)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = "SELECT * FROM customers WHERE name = @Name LIMIT 1";
        return await connection.QueryFirstOrDefaultAsync<CustomerDto>(sql, new { Name = name });
    }

}
