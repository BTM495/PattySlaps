using System;
using System.Windows;
using System.Windows.Controls;

namespace PattySlapsApp
{
    public partial class AddHireRequestWindow : Window
    {
        private readonly ApiService _apiService;

        public AddHireRequestWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newHireRequest = new Classes.HireRequest
                {
                    Date = DateTime.Now, // Default to today's date
                    Position = PositionTextBox.Text,
                    Status = "Unresolved",
                    StartingDate = StartingDatePicker.SelectedDate ?? DateTime.Now,
                    RequestingManager = RequestingManagerTextBox.Text
                };

                var response = await _apiService.AddHireRequestAsync(newHireRequest);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Hire request added successfully.");
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to add hire request. Server responded with: {errorMessage}");
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

