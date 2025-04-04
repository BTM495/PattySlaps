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
    /// Interaction logic for EditApplicationWindow.xaml
    /// </summary>
    public partial class EditApplicationWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly Classes.Application _application;

        public EditApplicationWindow(ApiService apiService, Classes.Application application)
        {
            InitializeComponent();
            _apiService = apiService;
            _application = application;

            // Populate fields with existing data
            ApplicationIDTextBox.Text = _application.ApplicationID.ToString();
            ApplicantIDTextBox.Text = _application.ApplicantID.ToString();
            PositionIDTextBox.Text = _application.PositionID.ToString();
            StatusTextBox.Text = _application.Status;
            SubmissionDateTextBox.Text = _application.SubmissionDate.ToString("d");
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _application.Status = StatusTextBox.Text;

                var response = await _apiService.UpdateApplicationAsync(_application.ApplicationID, _application);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Application updated successfully.");
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to update application. Server responded with: {errorMessage}");
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
