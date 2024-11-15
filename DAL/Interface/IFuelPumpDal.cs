public interface IFuelPumpDal
{
    Task<IEnumerable<FuelPumpDto>> GetAllAsync();
    Task<FuelPumpDto> GetByIdAsync(int id);
    Task InsertAsync(FuelPumpDto pump);
    Task UpdateAsync(FuelPumpDto pump);
    Task DeleteAsync(int id);
}