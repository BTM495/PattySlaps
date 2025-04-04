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
    /// Interaction logic for ShiftScheduleWindow.xaml
    /// </summary>
    public partial class ShiftScheduleWindow : Window
    {
        private readonly ApiService _apiService;
        private List<ShiftSchedule> _shiftSchedules;

        public ShiftScheduleWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            LoadShiftSchedules();
        }

        private async void LoadShiftSchedules()
        {
            try
            {
                _shiftSchedules = await _apiService.GetShiftSchedulesAsync();
                ShiftScheduleDataGrid.ItemsSource = _shiftSchedules;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading shift schedules: {ex.Message}");
            }
        }

        private void DateFilterPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ShiftFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filteredSchedules = _shiftSchedules.AsQueryable();

            if (DateFilterPicker.SelectedDate.HasValue)
            {
                var selectedDate = DateFilterPicker.SelectedDate.Value;
                filteredSchedules = filteredSchedules.Where(s => s.Date.Date == selectedDate.Date);
            }

            if (ShiftFilterComboBox.SelectedItem != null)
            {
                var selectedShift = (ShiftFilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                filteredSchedules = filteredSchedules.Where(s => s.Shift == selectedShift);
            }

            ShiftScheduleDataGrid.ItemsSource = filteredSchedules.ToList();
        }

        private void EditShiftSchedule_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var shiftSchedule = button.Tag as ShiftSchedule;
            var editWindow = new EditShiftWindow(_apiService, shiftSchedule);
            editWindow.ShowDialog();
            LoadShiftSchedules();
        }

        private async void DeleteShiftSchedule_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var shiftSchedule = button.Tag as ShiftSchedule;

            var result = MessageBox.Show("Are you sure you want to delete this shift schedule?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await _apiService.DeleteShiftScheduleAsync(shiftSchedule.ScheduleID);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Shift schedule deleted successfully.");
                        LoadShiftSchedules();
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Failed to delete shift schedule. Server responded with: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting shift schedule: {ex.Message}");
                }
            }
        }

        private void AddShift_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddShiftWindow(_apiService);
            addWindow.ShowDialog();
            LoadShiftSchedules();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
