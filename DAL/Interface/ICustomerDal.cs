public interface ICustomerDal
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto?> GetByIdAsync(int id);
    Task<CustomerDto?> GetByNameAsync(string name);
    Task<int> InsertAsync(CustomerDto customer);
    Task UpdateAsync(CustomerDto customer);
    Task DeleteAsync(int id);

}