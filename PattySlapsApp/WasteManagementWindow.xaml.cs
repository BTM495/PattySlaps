using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PattySlapsApp
{
    /// <summary>
    /// Interaction logic for WasteManagementWindow.xaml
    /// </summary>
    public partial class WasteManagementWindow : Window
    {
        private readonly ApiService _apiService;

        public WasteManagementWindow()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadWasteRecords();
        }

        private async void LoadWasteRecords()
        {
            try
            {
                var wasteRecords = await _apiService.GetWasteRecordsAsync();
                var items = await _apiService.GetItemsAsync();
                var itemDictionary = items.ToDictionary(item => item.ItemID);
                foreach (var record in wasteRecords)
                {
                    if (itemDictionary.TryGetValue(record.ItemID, out var item))
                    {
                        record.Item = item;
                    }

                }

                // Filter records by selected date
                if (DateFilterPicker.SelectedDate.HasValue)
                {
                    wasteRecords = wasteRecords.Where(r => r.Date.Date == DateFilterPicker.SelectedDate.Value.Date).ToList();
                }

                WasteDataGrid.ItemsSource = wasteRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load waste records: {ex.Message}");
            }
        }

        private void DateFilterPicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            var selectedDate = DateFilterPicker.SelectedDate;
            if (selectedDate.HasValue)
            {
                var filteredRecords = ((List<WasteRecord>)WasteDataGrid.ItemsSource)
                    .Where(record => record.Date.Date == selectedDate.Value.Date)
                    .ToList();
                WasteDataGrid.ItemsSource = filteredRecords;
            }
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            LoadWasteRecords();
        }

        private void EditWaste_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var wasteRecord = button?.Tag as WasteRecord;
            if (wasteRecord != null)
            {
                MessageBox.Show($"Edit Waste Record: {wasteRecord.WasteID}");
            }
        }

        private async void DeleteWaste_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var wasteRecord = button?.Tag as WasteRecord;
            if (wasteRecord != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete Waste Record: {wasteRecord.WasteID}?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var response = await _apiService.DeleteWasteRecordAsync(wasteRecord.WasteID);
                        if (response.IsSuccessStatusCode)
                        {
                            LoadWasteRecords();
                        }
                        else
                        {
                            MessageBox.Show($"Failed to delete waste record: {response.ReasonPhrase}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete waste record: {ex.Message}");
                    }
                }
            }
        }

        private async void AddWaste_Click(object sender, RoutedEventArgs e)
        {
            // Implement functionality to add new waste record
            var newWasteRecord = new WasteRecord
            {
                // Set properties for the new waste record
                InventoryRecordID = 1, // Example value
                Quantity = 10, // Example value
                WasteType = "Expired", // Example value
                Date = DateTime.Now // Example value
            };

            try
            {
                var response = await _apiService.AddWasteRecordAsync(newWasteRecord);
                if (response.IsSuccessStatusCode)
                {
                    LoadWasteRecords();
                }
                else
                {
                    MessageBox.Show($"Failed to add waste record: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add waste record: {ex.Message}");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
