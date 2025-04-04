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
    /// Interaction logic for HireRequestsWindow.xaml
    /// </summary>
    public partial class HireRequestsWindow : Window
    {
        private readonly ApiService _apiService;

        public HireRequestsWindow()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadHireRequests();
        }

        private async void LoadHireRequests()
        {
            try
            {
                var requests = await _apiService.GetHireRequestsAsync();

                // Filter requests by selected date
                if (DateFilterPicker.SelectedDate.HasValue)
                {
                    requests = requests.Where(r => r.Date.Date == DateFilterPicker.SelectedDate.Value.Date).ToList();
                }

                HireRequestDataGrid.ItemsSource = requests;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading hire requests: {ex.Message}");
            }
        }



        private void AddHireRequest_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddHireRequestWindow(_apiService);
            addWindow.ShowDialog();
            LoadHireRequests(); // Refresh requests after adding
        }

        private async void EditHireRequest_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is HireRequest request)
            {
                var editWindow = new EditHireRequestWindow(_apiService, request);
                editWindow.ShowDialog();
                LoadHireRequests(); // Refresh after editing
            }
        }

        private async void DeleteHireRequest_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is HireRequest request)
            {
                var result = MessageBox.Show($"Are you sure you want to delete request {request.RequestID}?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    await _apiService.DeleteHireRequestAsync(request.RequestID);
                    LoadHireRequests();
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DateFilterPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadHireRequests(); // Reload requests when the date filter changes
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            DateFilterPicker.SelectedDate = null;
            LoadHireRequests(); // Reload all requests
        }
    }
}
