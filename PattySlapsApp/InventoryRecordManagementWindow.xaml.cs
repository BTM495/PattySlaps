using System;
using System.Collections.Generic;
using System.Windows;
using PattySlapsApp.Classes;

namespace PattySlapsApp
{
    public partial class InventoryRecordManagementWindow : Window
    {
        private readonly ApiService _apiService;

        public InventoryRecordManagementWindow()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadInventoryRecords();
        }

        private async void LoadInventoryRecords()
        {
            try
            {
                var records = await _apiService.GetInventoryRecordsAsync();
                var items = await _apiService.GetItemsAsync();
                var itemDictionary = items.ToDictionary(item => item.ItemID);

                foreach (var record in records)
                {
                    if (itemDictionary.TryGetValue(record.ItemID, out var item))
                    {
                        record.Item = item;
                    }

                    // Calculate discrepancy flag
                    int totalWaste = record.WasteRecords?.Sum(w => w.Quantity) ?? 0;
                    record.DiscrepancyFlag = (record.SoDQuantity - record.QuantityUsed - totalWaste - record.EoDQuantity) != 0;
                }

                // Filter records by selected date
                if (DateFilterPicker.SelectedDate.HasValue)
                {
                    records = records.Where(r => r.Date.Date == DateFilterPicker.SelectedDate.Value.Date).ToList();
                }

                InventoryDataGrid.ItemsSource = records;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory records: {ex.Message}");
            }
        }

        private void AddInventory_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddInventoryWindow(_apiService);
            addWindow.ShowDialog();
            LoadInventoryRecords(); // Refresh inventory after adding
        }

        private async void EditInventory_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem is InventoryRecord record)
            {
                // Ensure the Item property is populated
                if (record.Item == null)
                {
                    record.Item = await _apiService.GetItemByIdAsync(record.ItemID);
                }

                var editWindow = new EditInventoryWindow(_apiService, record);
                editWindow.ShowDialog();
                LoadInventoryRecords(); // Refresh after editing
            }
        }

        private async void DeleteInventory_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryDataGrid.SelectedItem is InventoryRecord record)
            {
                var result = MessageBox.Show($"Are you sure you want to delete record {record.RecordID}?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    await _apiService.DeleteInventoryRecordAsync(record.RecordID);
                    LoadInventoryRecords();
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DateFilterPicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            LoadInventoryRecords(); // Reload records when the date filter changes
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            DateFilterPicker.SelectedDate = null;
            LoadInventoryRecords(); // Reload all records
        }
    }
}
