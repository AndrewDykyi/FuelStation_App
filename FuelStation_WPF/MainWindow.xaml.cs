using System.Windows;

namespace FuelStation_WPF
{
    public partial class MainWindow : Window
    {
        private readonly ICustomerDal _customerDal;
        private readonly IVehicleDal _vehicleDal;

        public MainWindow()
        {
            InitializeComponent();

            string connectionString = "Host=localhost;Database=Gas_Station_db;Username=postgres;Password=192837465qwerty@";

            _customerDal = new CustomerDal(connectionString);
            _vehicleDal = new VehicleDal(connectionString);
        }

        private async void LoadUsers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var users = await _customerDal.GetAllAsync();
                UsersListBox.ItemsSource = users.Select(u => new UserDisplayDto { CustomerId = u.CustomerId, Name = u.Name, Email = u.Email }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}");
            }
        }

        private async void LoadVehicles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vehicles = await _vehicleDal.GetAllAsync();
                VehiclesListBox.ItemsSource = vehicles.Select(v => new VehicleDisplayDto { VehicleId = v.VehicleId, Model = v.Model, LicensePlate = v.LicensePlate }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}");
            }
        }

        private async void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new CustomerDto
            {
                Name = UserNameTextBox.Text,
                Email = UserEmailTextBox.Text
            };
            await _customerDal.InsertAsync(newUser);
            MessageBox.Show("User added successfully.");
            LoadUsers_Click(sender, e);
        }

        private async void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedItem is UserDisplayDto selectedUser)
            {
                try
                {
                    var user = await _customerDal.GetByNameAsync(selectedUser.Name);

                    if (user != null)
                    {
                        await _customerDal.DeleteAsync(user.CustomerId);
                        MessageBox.Show("User deleted successfully.");
                        LoadUsers_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private async void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            var customerName = VehicleCustomerNameTextBox.Text;
            var customer = await _customerDal.GetByNameAsync(customerName);

            if (customer != null)
            {
                var newVehicle = new VehicleDto
                {
                    CustomerId = customer.CustomerId,
                    LicensePlate = VehicleLicensePlateTextBox.Text,
                    Model = VehicleModelTextBox.Text,
                    FuelType = VehicleFuelTypeTextBox.Text,
                    CreatedAt = DateTime.Now
                };

                await _vehicleDal.InsertAsync(newVehicle);
                MessageBox.Show("Vehicle added successfully.");
                LoadVehicles_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Customer not found.");
            }
        }

        private async void DeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (VehiclesListBox.SelectedItem is VehicleDisplayDto selectedVehicle)
            {
                string licensePlate = selectedVehicle.LicensePlate;
                await _vehicleDal.DeleteByLicensePlateAsync(licensePlate);
                MessageBox.Show("Vehicle deleted successfully.");
                LoadVehicles_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a vehicle to delete.");
            }
        }

    }
}
