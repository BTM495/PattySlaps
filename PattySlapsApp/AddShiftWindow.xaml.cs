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
    /// Interaction logic for AddShiftWindow.xaml
    /// </summary>
    public partial class AddShiftWindow : Window
    {
        private readonly ApiService _apiService;

        public AddShiftWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var shiftSchedule = new ShiftSchedule
                {
                    Date = DatePicker.SelectedDate ?? DateTime.Now,
                    Shift = (ShiftComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Status = StatusTextBox.Text,
                };

                var response = await _apiService.AddShiftScheduleAsync(shiftSchedule);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Shift added successfully.");
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to add shift. Server responded with: {errorMessage}");
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
    }
}
