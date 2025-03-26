using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PattySlapsApp
{
    public partial class AddEmployeeWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7143/api/") };

        public event Action EmployeeAdded; // Event to notify MainWindow
        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newEmployee = new
                {
                    EmployeeID = 0,  // API expects this field, but it will be assigned automatically
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    SIN = SINTextBox.Text,  // Replace with actual input if required
                    PhoneNumber = PhoneTextBox.Text,  // Replace with actual input
                    Email = EmailTextBox.Text,
                    Address = AddressTextBox.Text,  // Replace with actual input
                    BirthDate = BirthdayDatePicker.SelectedDate,  // Adjust if user selects date
                    Role = RoleTextBox.Text,
                    EmploymentStatus = EmploymentStatTextBox.Text,  // Replace with dropdown selection
                    EmploymentType = EmploymentTypeTextBox.Text,  // Replace with dropdown selection
                    Wage = decimal.Parse(WageTextBox.Text),  // Default wage (adjust as needed)
                    BranchID = int.Parse(BranchIDTextBox.Text),
                };

                string json = JsonSerializer.Serialize(newEmployee);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("Employee", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Employee added successfully!");
                    EmployeeAdded?.Invoke();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add employee: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
