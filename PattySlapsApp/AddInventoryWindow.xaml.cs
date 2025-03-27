using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PattySlapsApp
{
    public partial class AddInventoryWindow : Window
    {
        private readonly ApiService _apiService;

        public AddInventoryWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            LoadItems();
        }

        private async void LoadItems()
        {
            try
            {
                var itemsList = await _apiService.GetItemsAsync(); // ✅ Already returns List<Item>
                ItemComboBox.ItemsSource = itemsList;
                ItemComboBox.DisplayMemberPath = "Name";
                ItemComboBox.SelectedValuePath = "ItemID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}");
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            var selectedItemId = (int)ItemComboBox.SelectedValue;
            var existingRecords = await _apiService.GetInventoryRecordsAsync();
            var existingRecordForToday = existingRecords.FirstOrDefault(record => record.ItemID == selectedItemId && record.Date.Date == DateTime.Now.Date);

            if (existingRecordForToday != null)
            {
                MessageBox.Show("An inventory record for this item has already been added today. Please edit the existing record instead.");
                return;
            }

            var newInventoryRecord = new InventoryRecord
            {
                ItemID = selectedItemId,
                Date = DateTime.Now,
                Time = DateTime.Now.ToString("HH:mm"),
                SoDQuantity = int.Parse(SoDQuantityTextBox.Text),
                EoDQuantity = int.Parse(EoDQuantityTextBox.Text),
                QuantityUsed = int.Parse(QuantityUsedTextBox.Text),
            };

            var response = await _apiService.AddInventoryRecordAsync(newInventoryRecord);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Inventory record added successfully.");
                Close();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Failed to add inventory record. Error: {response.StatusCode} - {errorContent}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
