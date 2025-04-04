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
    /// Interaction logic for EditQCChecklistWindow.xaml
    /// </summary>
    public partial class EditQCChecklistWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly QCChecklist _qcChecklist;

        public EditQCChecklistWindow(ApiService apiService, QCChecklist qcChecklist)
        {
            InitializeComponent();
            _apiService = apiService;
            _qcChecklist = qcChecklist;

            // Populate fields with existing data
            QCIDTextBox.Text = _qcChecklist.QCID.ToString();
            DateTextBox.Text = _qcChecklist.Date.ToString("d");
            ItemIDTextBox.Text = _qcChecklist.ItemID?.ToString();
            ItemNameTextBox.Text = _qcChecklist.ItemName;
            ItemDefectTextBox.Text = _qcChecklist.ItemDefect;
            QuantityTextBox.Text = _qcChecklist.Quantity.ToString();
            ItemPictureTextBox.Text = _qcChecklist.ItemPicture;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _qcChecklist.ItemDefect = ItemDefectTextBox.Text;
                _qcChecklist.Quantity = int.Parse(QuantityTextBox.Text);
                _qcChecklist.ItemPicture = ItemPictureTextBox.Text;

                var response = await _apiService.UpdateQCChecklistAsync(_qcChecklist.QCID.Value, _qcChecklist);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("QC Checklist updated successfully.");
                    this.Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to update QC Checklist. Server responded with: {errorMessage}");
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
