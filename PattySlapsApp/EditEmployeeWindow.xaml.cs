using PattySlapsApp.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PattySlapsApp
{
    public partial class EditEmployeeWindow : Window
    {
        private readonly ApiService _apiService;
        private Employee _employee;

        public EditEmployeeWindow(Employee employee, ApiService apiService) // ✅ Add constructor parameters
        {
            InitializeComponent();
            _apiService = apiService; // ✅ Store API service reference
            _employee = employee;

            // Populate fields with employee data
            FirstNameTextBox.Text = employee.FirstName;
            LastNameTextBox.Text = employee.LastName;
            SINTextBox.Text = employee.SIN;
            PhoneTextBox.Text = employee.PhoneNumber;
            EmailTextBox.Text = employee.Email;
            AddressTextBox.Text = employee.Address;
            BirthdayDatePicker.SelectedDate = employee.BirthDate;
            RoleTextBox.Text = employee.Role;
            EmploymentStatTextBox.Text = employee.EmploymentStatus;
            EmploymentTypeTextBox.Text = employee.EmploymentType;
            WageTextBox.Text = employee.Wage.ToString();
            BranchIDTextBox.Text = employee.BranchID?.ToString();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Update _employee object with new values from form
                _employee.EmployeeID = _employee.EmployeeID;
                _employee.FirstName = FirstNameTextBox.Text;
                _employee.LastName = LastNameTextBox.Text;
                _employee.SIN = SINTextBox.Text;
                _employee.PhoneNumber = PhoneTextBox.Text;
                _employee.Email = EmailTextBox.Text;
                _employee.Address = AddressTextBox.Text;
                _employee.BirthDate = BirthdayDatePicker.SelectedDate;
                _employee.Role = RoleTextBox.Text;
                _employee.EmploymentStatus = EmploymentStatTextBox.Text;
                _employee.EmploymentType = EmploymentTypeTextBox.Text;
                _employee.Wage = decimal.Parse(WageTextBox.Text);
                _employee.BranchID = int.Parse(BranchIDTextBox.Text);

                var response = await _apiService.UpdateEmployeeAsync(_employee);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Employee updated successfully.");
                    this.DialogResult = true; // Close the window
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to update employee. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating employee: " + ex.Message);
            }
        }

    }
}