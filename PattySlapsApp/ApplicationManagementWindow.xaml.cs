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
    /// Interaction logic for ApplicationManagementWindow.xaml
    /// </summary>
    public partial class ApplicationManagementWindow : Window
    {
        private readonly ApiService _apiService;

        public ApplicationManagementWindow()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadApplications();
        }

        private async void LoadApplications()
        {
            try
            {
                var applications = await _apiService.GetApplicationsAsync();

                // Filter applications by selected date
                if (DateFilterPicker.SelectedDate.HasValue)
                {
                    applications = applications.Where(a => a.SubmissionDate.Date == DateFilterPicker.SelectedDate.Value.Date).ToList();
                }

                ApplicationDataGrid.ItemsSource = applications;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading applications: {ex.Message}");
            }
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            DateFilterPicker.SelectedDate = null;
            LoadApplications(); // Reload all applications
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DateFilterPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadApplications(); // Reload applications when the date filter changes
        }

        private async void EditApplication_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Classes.Application application)
            {
                var editWindow = new EditApplicationWindow(_apiService, application);
                editWindow.ShowDialog();
                LoadApplications(); // Refresh after editing
            }
        }

        private async void DeleteApplication_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is Classes.Application application)
            {
                var result = MessageBox.Show($"Are you sure you want to delete application {application.ApplicationID}?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    await _apiService.DeleteApplicationAsync(application.ApplicationID);
                    LoadApplications();
                }
            }
        }

        private void ApplicationDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ApplicationDataGrid.SelectedItem is Classes.Application application)
            {
                var infoWindow = new AppInfoWindow(_apiService, application);
                infoWindow.ShowDialog();
            }
        }
    }
}
