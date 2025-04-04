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
    /// Interaction logic for QCChecklistWindow.xaml
    /// </summary>
    public partial class QCChecklistWindow : Window
    {
        private readonly ApiService _apiService;
        private List<QCChecklist> _qcChecklists;

        public QCChecklistWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            LoadQCChecklists();
        }

        private async void LoadQCChecklists()
        {
            try
            {
                _qcChecklists = await _apiService.GetQCChecklistsAsync();
                QCChecklistDataGrid.ItemsSource = _qcChecklists;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading QC Checklists: {ex.Message}");
            }
        }

        private async void DateFilterPicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DateFilterPicker.SelectedDate.HasValue)
            {
                var selectedDate = DateFilterPicker.SelectedDate.Value;
                var filteredChecklists = _qcChecklists.FindAll(qc => qc.Date.Date == selectedDate.Date);
                QCChecklistDataGrid.ItemsSource = filteredChecklists;
            }
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            QCChecklistDataGrid.ItemsSource = _qcChecklists;
        }

        private void EditQCChecklist_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var qcChecklist = button.Tag as QCChecklist;
            var editWindow = new EditQCChecklistWindow(_apiService, qcChecklist);
            editWindow.ShowDialog();
            LoadQCChecklists();
        }
        private async void GenerateQCChecklist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = await _apiService.GetItemsAsync();
                foreach (var item in items)
                {
                    // Check if a QC Checklist already exists for the same ItemID and Date
                    bool checklistExists = _qcChecklists.Any(qc => qc.ItemID == item.ItemID && qc.Date.Date == DateTime.Now.Date);
                    if (!checklistExists)
                    {
                        var newQCChecklist = new QCChecklist
                        {
                            QCID = 0, // API expects this field, but it will be assigned automatically
                            Date = DateTime.Now,
                            ItemID = item.ItemID,
                            ItemName = item.Name,
                            ItemDefect = "N/A",
                            Quantity = 0,
                            ItemPicture = "N/A",
                            Completed = false
                        };

                        var response = await _apiService.AddQCChecklistAsync(newQCChecklist);
                        if (!response.IsSuccessStatusCode)
                        {
                            string errorMessage = await response.Content.ReadAsStringAsync();
                            MessageBox.Show($"Failed to generate QC Checklist for item {item.Name}. Server responded with: {errorMessage}");
                        }
                    }
                }

                MessageBox.Show("QC Checklists generated successfully.");
                LoadQCChecklists();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QC Checklists: {ex.Message}");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
