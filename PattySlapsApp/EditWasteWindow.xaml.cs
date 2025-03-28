using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace PattySlapsApp
{
    public partial class EditWasteWindow : Window
    {
        private readonly ApiService _apiService;
        private WasteRecord _wasteRecord;

        public EditWasteWindow(ApiService apiService, WasteRecord wasteRecord)
        {
            InitializeComponent();
            _apiService = apiService;
            _wasteRecord = wasteRecord;

            // Populate fields
            ItemNameTextBlock.Text = wasteRecord.Item.Name;
            WasteTypeTextBox.Text = wasteRecord.WasteType;
            QuantityTextBox.Text = wasteRecord.Quantity.ToString();

            // Fetch and assign InventoryRecord
            LoadInventoryRecordAsync(wasteRecord.InventoryRecordID);
        }

        private async void LoadInventoryRecordAsync(int inventoryRecordID)
        {
            try
            {
                _wasteRecord.InventoryRecord = await _apiService.GetInventoryRecordByIdAsync(inventoryRecordID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load InventoryRecord: {ex.Message}");
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _wasteRecord.WasteType = WasteTypeTextBox.Text;
                _wasteRecord.Quantity = int.Parse(QuantityTextBox.Text);
                _wasteRecord.Date = DateTime.Now; // Assuming current date for the waste record
                _wasteRecord.ItemID = _wasteRecord.Item.ItemID;
                _wasteRecord.InventoryRecordID = _wasteRecord.InventoryRecord.RecordID;

                HttpResponseMessage response = await _apiService.UpdateWasteRecordAsync(_wasteRecord.WasteID, _wasteRecord);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Waste record updated successfully.");
                    Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(errorMessage))
                    {
                        errorMessage = "No error message provided by the server.";
                    }
                    MessageBox.Show($"Failed to update waste record. Server responded with: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
