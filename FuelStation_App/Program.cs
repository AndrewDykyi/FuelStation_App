using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        var connectionString = "Host=localhost;Database=Gas_Station_db;Username=postgres;Password=192837465qwerty@";

        var host = CreateHostBuilder(args, connectionString).Build();

        await host.StartAsync();

        var customerDal = host.Services.GetRequiredService<ICustomerDal>();
        var vehicleDal = host.Services.GetRequiredService<IVehicleDal>();
        var fuelStationDal = host.Services.GetRequiredService<IFuelStationDal>();
        var employeeDal = host.Services.GetRequiredService<IEmployeeDal>();

        int option = -1;

        while (option != 0)
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. List all Customers");
            Console.WriteLine("2. Insert Customer");
            Console.WriteLine("3. Delete Customer");
            Console.WriteLine("4. List all Vehicles");
            Console.WriteLine("5. Insert Vehicle");
            Console.WriteLine("6. Delete Vehicle");
            Console.WriteLine("7. List all Fuel Stations");
            Console.WriteLine("8. Insert Fuel Station");
            Console.WriteLine("9. Delete Fuel Station");
            Console.WriteLine("10. List all Employees");
            Console.WriteLine("11. Insert Employee");
            Console.WriteLine("12. Delete Employee");
            Console.WriteLine("0. Exit");

            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        await ListAllCustomers(customerDal);
                        break;
                    case 2:
                        await InsertCustomer(customerDal);
                        break;
                    case 3:
                        await DeleteCustomer(customerDal);
                        break;
                    case 4:
                        await ListAllVehicles(vehicleDal);
                        break;
                    case 5:
                        await InsertVehicle(vehicleDal);
                        break;
                    case 6:
                        await DeleteVehicle(vehicleDal);
                        break;
                    case 7:
                        await ListAllFuelStations(fuelStationDal);
                        break;
                    case 8:
                        await InsertFuelStation(fuelStationDal);
                        break;
                    case 9:
                        await DeleteFuelStation(fuelStationDal);
                        break;
                    case 10:
                        await ListAllEmployees(employeeDal);
                        break;
                    case 11:
                        await InsertEmployee(employeeDal);
                        break;
                    case 12:
                        await DeleteEmployee(employeeDal);
                        break;
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args, string connectionString) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<ICustomerDal>(new CustomerDal(connectionString));
                services.AddSingleton<IVehicleDal>(new VehicleDal(connectionString));
                services.AddSingleton<IFuelStationDal>(new FuelStationDal(connectionString));
                services.AddSingleton<IEmployeeDal>(new EmployeeDal(connectionString));
            });

    private static async Task ListAllCustomers(ICustomerDal customerDal)
    {
        var customers = await customerDal.GetAllAsync();
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.CustomerId}, Name: {customer.Name}, Email: {customer.Email}");
        }
    }

    private static async Task InsertCustomer(ICustomerDal customerDal)
    {
        var customer = new CustomerDto();
        Console.Write("Enter Customer Name: ");
        customer.Name = Console.ReadLine();
        Console.Write("Enter Customer Email: ");
        customer.Email = Console.ReadLine();

        await customerDal.InsertAsync(customer);
        Console.WriteLine("Customer added successfully.");
    }

    private static async Task DeleteCustomer(ICustomerDal customerDal)
    {
        Console.Write("Enter Customer ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            await customerDal.DeleteAsync(id);
            Console.WriteLine("Customer deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static async Task ListAllVehicles(IVehicleDal vehicleDal)
    {
        var vehicles = await vehicleDal.GetAllAsync();
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine($"ID: {vehicle.VehicleId}, Model: {vehicle.Model}, License Plate: {vehicle.LicensePlate}");
        }
    }

    private static async Task InsertVehicle(IVehicleDal vehicleDal)
    {
        var vehicle = new VehicleDto();
        Console.Write("Enter Customer ID: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid Customer ID.");
            return;
        }
        vehicle.CustomerId = customerId;

        Console.Write("Enter Vehicle Model: ");
        vehicle.Model = Console.ReadLine();
        Console.Write("Enter Vehicle License Plate: ");
        vehicle.LicensePlate = Console.ReadLine();
        Console.Write("Enter Vehicle Fuel Type: ");
        vehicle.FuelType = Console.ReadLine();

        int vehicleId = await vehicleDal.InsertAsync(vehicle);
        Console.WriteLine($"Vehicle added successfully with ID: {vehicleId}");
    }

    private static async Task DeleteVehicle(IVehicleDal vehicleDal)
    {
        Console.Write("Enter Vehicle ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            await vehicleDal.DeleteAsync(id);
            Console.WriteLine("Vehicle deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static async Task ListAllFuelStations(IFuelStationDal stationDal)
    {
        var stations = await stationDal.GetAllAsync();
        foreach (var station in stations)
        {
            Console.WriteLine($"ID: {station.FuelStationId}, Name: {station.Name}, Location: {station.Location}");
        }
    }

    private static async Task InsertFuelStation(IFuelStationDal fuelStationDal)
    {
        var station = new FuelStationDto();
        Console.Write("Enter Fuel Station Name: ");
        station.Name = Console.ReadLine();
        Console.Write("Enter Fuel Station Location: ");
        station.Location = Console.ReadLine();

        await fuelStationDal.InsertAsync(station);
        Console.WriteLine("Fuel Station added successfully.");
    }

    private static async Task DeleteFuelStation(IFuelStationDal fuelStationDal)
    {
        Console.Write("Enter Fuel Station ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            await fuelStationDal.DeleteAsync(id);
            Console.WriteLine("Fuel Station deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static async Task ListAllEmployees(IEmployeeDal employeeDal)
    {
        var employees = await employeeDal.GetAllAsync();
        foreach (var employee in employees)
        {
            Console.WriteLine($"ID: {employee.EmployeeId}, Name: {employee.Name}, Position: {employee.Position}");
        }
    }

    private static async Task InsertEmployee(IEmployeeDal employeeDal)
    {
        var employee = new EmployeeDto();

        Console.Write("Enter Employee Name: ");
        employee.Name = Console.ReadLine();

        Console.Write("Enter Employee Position: ");
        employee.Position = Console.ReadLine();

        Console.Write("Enter Station ID: ");
        if (int.TryParse(Console.ReadLine(), out int stationId))
        {
            employee.StationId = stationId;
        }
        else
        {
            Console.WriteLine("Invalid Station ID.");
            return;
        }

        employee.HireDate = DateTime.Now;

        await employeeDal.InsertAsync(employee);
        Console.WriteLine($"Employee added successfully. Hire Date: {employee.HireDate:yyyy-MM-dd HH:mm:ss}");
    }


    private static async Task DeleteEmployee(IEmployeeDal employeeDal)
    {
        Console.Write("Enter Employee ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            await employeeDal.DeleteAsync(id);
            Console.WriteLine("Employee deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }
}