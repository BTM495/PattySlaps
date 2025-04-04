using PattySlapsApp.Classes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PattySlapsApp
{
    public partial class EmployeeManagementWindow : Window
    {
        private readonly ApiService _apiService = new ApiService();
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7143/api/") };

        public EmployeeManagementWindow()
        {
            InitializeComponent();
            LoadEmployees();
        }

        public async void LoadEmployees()
        {
            try
            {
                List<Employee> employees = await _apiService.GetEmployeesAsync();
                EmployeeDataGrid.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employees: " + ex.Message);
            }
        }
        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.EmployeeAdded += LoadEmployees;
            addEmployeeWindow.ShowDialog();
        }
        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Employee selectedEmployee)
            {
                EditEmployeeWindow editWindow = new EditEmployeeWindow(selectedEmployee, _apiService);
                editWindow.ShowDialog();
                LoadEmployees(); // Refresh after editing
            }
        }

        private async void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Employee selectedEmployee)
            {
                // 🔍 Print Employee ID in confirmation box
                var result = MessageBox.Show($"Are you sure you want to delete {selectedEmployee.FirstName} {selectedEmployee.LastName}? \n\nEmployee ID: {selectedEmployee.EmployeeID}",
                                             "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // 🔍 Print Employee ID to Console
                        Console.WriteLine($"Sending Delete Request for Employee ID: {selectedEmployee.EmployeeID}");

                        var response = await _apiService.DeleteEmployeeAsync(selectedEmployee.EmployeeID);

                        // 🔍 Log API Response
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"API Response Status: {response.StatusCode}");
                        Console.WriteLine($"API Response Content: {responseContent}");

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show($"Employee ID {selectedEmployee.EmployeeID} deleted successfully.");
                            LoadEmployees(); // Refresh the list
                        }
                        else
                        {
                            MessageBox.Show($"Failed to delete employee.\nStatus: {response.StatusCode}\nError: {responseContent}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Unexpected error: {ex.Message}");
                    }
                }
            }
        }



    }
}
