using DAL.Interface;
using System.Data.SqlClient;

public class FuelInventoryDAL : IFuelInventoryDAL
{
    private readonly string _connectionString;

    public FuelInventoryDAL(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<FuelInventoryDTO> GetAllFuelInventory()
    {
        var fuelInventoryList = new List<FuelInventoryDTO>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT inventory_id, station_id, fuel_type, quantity, last_updated FROM fuel_inventory", connection);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    fuelInventoryList.Add(new FuelInventoryDTO
                    {
                        InventoryId = reader.GetInt32(0),
                        StationId = reader.GetInt32(1),
                        FuelType = reader.GetString(2),
                        Quantity = reader.GetDouble(3),
                        LastUpdated = reader.GetDateTime(4)
                    });
                }
            }
        }
        return fuelInventoryList;
    }

    public FuelInventoryDTO GetFuelInventoryById(int inventoryId)
    {
        FuelInventoryDTO fuelInventory = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT inventory_id, station_id, fuel_type, quantity, last_updated FROM fuel_inventory WHERE inventory_id = @inventoryId", connection);
            command.Parameters.AddWithValue("@inventoryId", inventoryId);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    fuelInventory = new FuelInventoryDTO
                    {
                        InventoryId = reader.GetInt32(0),
                        StationId = reader.GetInt32(1),
                        FuelType = reader.GetString(2),
                        Quantity = reader.GetDouble(3),
                        LastUpdated = reader.GetDateTime(4)
                    };
                }
            }
        }
        return fuelInventory;
    }

    public void InsertFuelInventory(FuelInventoryDTO fuelInventory)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand(
                "INSERT INTO fuel_inventory (station_id, fuel_type, quantity, last_updated) VALUES (@stationId, @fuelType, @quantity, @lastUpdated)", connection);
            command.Parameters.AddWithValue("@stationId", fuelInventory.StationId);
            command.Parameters.AddWithValue("@fuelType", fuelInventory.FuelType);
            command.Parameters.AddWithValue("@quantity", fuelInventory.Quantity);
            command.Parameters.AddWithValue("@lastUpdated", fuelInventory.LastUpdated);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void UpdateFuelInventory(FuelInventoryDTO fuelInventory)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand(
                "UPDATE fuel_inventory SET station_id = @stationId, fuel_type = @fuelType, quantity = @quantity, last_updated = @lastUpdated WHERE inventory_id = @inventoryId", connection);
            command.Parameters.AddWithValue("@stationId", fuelInventory.StationId);
            command.Parameters.AddWithValue("@fuelType", fuelInventory.FuelType);
            command.Parameters.AddWithValue("@quantity", fuelInventory.Quantity);
            command.Parameters.AddWithValue("@lastUpdated", fuelInventory.LastUpdated);
            command.Parameters.AddWithValue("@inventoryId", fuelInventory.InventoryId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void DeleteFuelInventory(int inventoryId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("DELETE FROM fuel_inventory WHERE inventory_id = @inventoryId", connection);
            command.Parameters.AddWithValue("@inventoryId", inventoryId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
