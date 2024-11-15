namespace DAL.Interface
{
    public interface IFuelInventoryDAL
    {
        IEnumerable<FuelInventoryDTO> GetAllFuelInventory();
        FuelInventoryDTO GetFuelInventoryById(int inventoryId);
        void InsertFuelInventory(FuelInventoryDTO fuelInventory);
        void UpdateFuelInventory(FuelInventoryDTO fuelInventory);
        void DeleteFuelInventory(int inventoryId);
    }

}
