public interface IEmployeeDal
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto> GetByIdAsync(int id);
    Task InsertAsync(EmployeeDto employee);
    Task UpdateAsync(EmployeeDto employee);
    Task DeleteAsync(int id);
}