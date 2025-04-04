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
    /// Interaction logic for AppInfoWindow.xaml
    /// </summary>
    public partial class AppInfoWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly Classes.Application _application;

        public AppInfoWindow(ApiService apiService, Classes.Application application)
        {
            InitializeComponent();
            _apiService = apiService;
            _application = application;

            // Populate application fields with existing data
            ApplicationIDTextBox.Text = _application.ApplicationID.ToString();
            ApplicantIDTextBox.Text = _application.ApplicantID.ToString();
            PositionIDTextBox.Text = _application.PositionID.ToString();
            StatusTextBox.Text = _application.Status;
            SubmissionDateTextBox.Text = _application.SubmissionDate.ToString("d");

            // Fetch and populate applicant data
            LoadApplicantData(_application.ApplicantID);
        }

        private async void LoadApplicantData(int applicantId)
        {
            try
            {
                var applicant = await _apiService.GetApplicantByIdAsync(applicantId);
                if (applicant != null)
                {
                    PersonalInfoTextBox.Text = applicant.PersonalInfo;
                    EducationLevelTextBox.Text = applicant.EducationLevel;
                    ExperienceTextBox.Text = applicant.Experience;
                    AvailabilityTextBox.Text = applicant.Availability;
                    HourPreferencesTextBox.Text = applicant.HourPreferences;
                    ResumeTextBox.Text = applicant.Resume;
                }
                else
                {
                    MessageBox.Show("Applicant data not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching applicant data: {ex.Message}");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
