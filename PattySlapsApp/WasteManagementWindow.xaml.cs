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

        private void AddWaste_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddWasteWindow(_apiService);
            addWindow.ShowDialog();
            LoadWasteRecords(); // Refresh waste records after adding
        }

        private async void EditWaste_Click(object sender, RoutedEventArgs e)
        {
            if (WasteDataGrid.SelectedItem is WasteRecord wasteRecord)
            {
                // Ensure the Item property is populated
                if (wasteRecord.Item == null)
                {
                    wasteRecord.Item = await _apiService.GetItemByIdAsync(wasteRecord.ItemID);
                }

                var editWindow = new EditWasteWindow(_apiService, wasteRecord);
                editWindow.ShowDialog();
                LoadWasteRecords(); // Refresh after editing
            }
        }

        private async void DeleteWaste_Click(object sender, RoutedEventArgs e)
        {
            if (WasteDataGrid.SelectedItem is WasteRecord wasteRecord)
            {
                var result = MessageBox.Show($"Are you sure you want to delete waste record {wasteRecord.WasteID}?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    await _apiService.DeleteWasteRecordAsync(wasteRecord.WasteID);
                    LoadWasteRecords();
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DateFilterPicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            LoadWasteRecords(); // Reload records when the date filter changes
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            DateFilterPicker.SelectedDate = null;
            LoadWasteRecords(); // Reload all records
        }
    }
}
