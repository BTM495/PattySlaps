using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PattySlapsApp
{
    public partial class EditInventoryWindow : Window
    {
        private readonly ApiService _apiService;
        private InventoryRecord _inventoryRecord;

        public EditInventoryWindow(ApiService apiService, InventoryRecord inventoryRecord)
        {
            InitializeComponent();
            _apiService = apiService;
            _inventoryRecord = inventoryRecord;

            // Populate fields
            ItemNameTextBlock.Text = inventoryRecord.Item.Name;
            SoDQuantityTextBox.Text = inventoryRecord.SoDQuantity.ToString();
            EoDQuantityTextBox.Text = inventoryRecord.EoDQuantity.ToString();
            QuantityUsedTextBox.Text = inventoryRecord.QuantityUsed.ToString();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _inventoryRecord.SoDQuantity = int.Parse(SoDQuantityTextBox.Text);
                _inventoryRecord.EoDQuantity = int.Parse(EoDQuantityTextBox.Text);
                _inventoryRecord.QuantityUsed = int.Parse(QuantityUsedTextBox.Text);

                if (_inventoryRecord.WasteRecords.Count > 0)
                    _inventoryRecord.WasteRecords[0].Quantity = int.Parse(WasteQuantityTextBox.Text);
                else
                    _inventoryRecord.WasteRecords.Add(new WasteRecord { Quantity = int.Parse(WasteQuantityTextBox.Text) });

                HttpResponseMessage response = await _apiService.UpdateInventoryRecordAsync(_inventoryRecord.RecordID, _inventoryRecord);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Inventory record updated successfully.");
                    Close();
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(errorMessage))
                    {
                        errorMessage = "No error message provided by the server.";
                    }
                    MessageBox.Show($"Failed to update inventory record. Server responded with: {errorMessage}");
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
