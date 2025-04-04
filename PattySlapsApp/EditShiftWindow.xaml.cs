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
    /// Interaction logic for EditShiftWindow.xaml
    /// </summary>
    public partial class EditShiftWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly int _shiftScheduleId;

        public EditShiftWindow(ApiService apiService, ShiftSchedule shiftSchedule)
        {
            InitializeComponent();
            _apiService = apiService;
            _shiftScheduleId = shiftSchedule.ScheduleID;
            LoadShiftDetails();
        }

        private async void LoadShiftDetails()
        {
            try
            {
                var shiftSchedule = await _apiService.GetShiftScheduleByIdAsync(_shiftScheduleId);
                DatePicker.SelectedDate = shiftSchedule.Date;
                ShiftComboBox.SelectedItem = ShiftComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == shiftSchedule.Shift);

                var shiftScheduleEmployees = await _apiService.GetShiftScheduleEmployeesAsync();
                var scheduledEmployeeIds = shiftScheduleEmployees.Where(e => e.ScheduleID == _shiftScheduleId).Select(e => e.EmployeeID).ToList();

                var employees = await _apiService.GetEmployeesAsync();
                var scheduledEmployees = employees.Where(e => scheduledEmployeeIds.Contains(e.EmployeeID)).ToList();
                EmployeesDataGrid.ItemsSource = scheduledEmployees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shift details: {ex.Message}");
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var shiftSchedule = new ShiftSchedule
                {
                    Date = DatePicker.SelectedDate ?? DateTime.Now,
                    Shift = (ShiftComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                };

                var response = await _apiService.UpdateShiftScheduleAsync(_shiftScheduleId, shiftSchedule);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Shift updated successfully.");
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to update shift. Server responded with: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeToScheduleWindow(_apiService, _shiftScheduleId);
            addEmployeeWindow.ShowDialog();
            LoadShiftDetails(); // Refresh the shift details after adding an employee
        }
    }
}
