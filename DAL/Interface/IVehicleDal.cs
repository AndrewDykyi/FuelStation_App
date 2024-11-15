public interface IVehicleDal
{
    Task<IEnumerable<VehicleDto>> GetAllAsync();
    Task<VehicleDto> GetByIdAsync(int id);
    Task DeleteByLicensePlateAsync(string licensePlate);

    Task<int> InsertAsync(VehicleDto vehicle);
    Task UpdateAsync(VehicleDto vehicle);
    Task DeleteAsync(int id);
}
