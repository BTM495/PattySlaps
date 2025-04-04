using System;
using System.Collections.Generic;
using System.Linq;
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
using PattySlapsApp.Classes;

namespace PattySlapsApp
{
    /// <summary>
    /// Interaction logic for AddEmployeetoScheduleWindow.xaml
    /// </summary>
    public partial class AddEmployeeToScheduleWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly int _shiftScheduleId;

        public AddEmployeeToScheduleWindow(ApiService apiService, int shiftScheduleId)
        {
            InitializeComponent();
            _apiService = apiService;
            _shiftScheduleId = shiftScheduleId;
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            try
            {
                var employees = await _apiService.GetEmployeesAsync();
                EmployeeDataGrid.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}");
            }
        }

        private async void EmployeeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                try
                {
                    var shiftScheduleEmployees = await _apiService.GetShiftScheduleEmployeesAsync();
                    var isEmployeeAlreadyScheduled = shiftScheduleEmployees.Any(e => e.ScheduleID == _shiftScheduleId && e.EmployeeID == selectedEmployee.EmployeeID);

                    if (isEmployeeAlreadyScheduled)
                    {
                        MessageBox.Show("This employee is already scheduled for this shift.");
                        return;
                    }

                    var shiftScheduleEmployee = new ShiftScheduleEmployee
                    {
                        EmployeeID = selectedEmployee.EmployeeID,
                        ScheduleID = _shiftScheduleId
                    };

                    var response = await _apiService.AddShiftScheduleEmployeeAsync(shiftScheduleEmployee);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Employee added to shift successfully.");
                        this.Close();
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to add employee to shift. Server responded with: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }
    }
}
