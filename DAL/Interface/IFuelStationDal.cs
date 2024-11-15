public interface IFuelStationDal
{
    Task<IEnumerable<FuelStationDto>> GetAllAsync();
    Task<FuelStationDto> GetByIdAsync(int id);
    Task InsertAsync(FuelStationDto station);
    Task UpdateAsync(FuelStationDto station);
    Task DeleteAsync(int id);
}