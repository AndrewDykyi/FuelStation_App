public interface IFuelTransactionDal
{
    Task<IEnumerable<FuelTransactionDto>> GetAllAsync();
    Task<FuelTransactionDto> GetByIdAsync(int id);
    Task InsertAsync(FuelTransactionDto transaction);
    Task UpdateAsync(FuelTransactionDto transaction);
    Task DeleteAsync(int id);
}