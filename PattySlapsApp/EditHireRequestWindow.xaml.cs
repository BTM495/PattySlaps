using PattySlapsApp.Classes;
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

namespace PattySlapsApp
{
    /// <summary>
    /// Interaction logic for EditHireRequestWindow.xaml
    /// </summary>
    public partial class EditHireRequestWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly HireRequest _hireRequest;

        public EditHireRequestWindow(ApiService apiService, HireRequest hireRequest)
        {
            InitializeComponent();
            _apiService = apiService;
            _hireRequest = hireRequest;

            // Populate fields with existing data
            RequestIDTextBox.Text = _hireRequest.RequestID.ToString();
            RequestingManagerTextBox.Text = _hireRequest.RequestingManager;
            DateTextBox.Text = _hireRequest.Date.ToString("d");

            PositionTextBox.Text = _hireRequest.Position;
            StatusTextBox.Text = _hireRequest.Status;
            StartingDatePicker.SelectedDate = _hireRequest.StartingDate;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _hireRequest.Position = PositionTextBox.Text;
                _hireRequest.Status = StatusTextBox.Text;
                _hireRequest.StartingDate = StartingDatePicker.SelectedDate ?? DateTime.Now;

                var response = await _apiService.UpdateHireRequestAsync(_hireRequest.RequestID, _hireRequest);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Hire request updated successfully.");
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to update hire request. Server responded with: {errorMessage}");
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
