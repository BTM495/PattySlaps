using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace PattySlapsApp
{
    public partial class AddWasteWindow : Window
    {
        private readonly ApiService _apiService;

        public AddWasteWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            LoadItems();
        }

        private async void LoadItems()
        {
            try
            {
                var itemsList = await _apiService.GetItemsAsync();
                ItemComboBox.ItemsSource = itemsList;
                ItemComboBox.DisplayMemberPath = "Name";
                ItemComboBox.SelectedValuePath = "ItemID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading items: {ex.Message}");
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            var selectedItemId = (int)ItemComboBox.SelectedValue;
            var existingInventoryRecords = await _apiService.GetInventoryRecordsAsync();
            var existingInventoryRecordForToday = existingInventoryRecords.FirstOrDefault(record => record.ItemID == selectedItemId && record.Date.Date == DateTime.Now.Date);

            if (existingInventoryRecordForToday == null)
            {
                MessageBox.Show("No inventory record found for this item today. Please add an inventory record first.");
                var addInventoryWindow = new AddInventoryWindow(_apiService);
                addInventoryWindow.ShowDialog();
                return;
            }

            var existingWasteRecords = await _apiService.GetWasteRecordsAsync();
            var existingWasteRecordForToday = existingWasteRecords.FirstOrDefault(record => record.ItemID == selectedItemId && record.Date.Date == DateTime.Now.Date);

            if (existingWasteRecordForToday != null)
            {
                MessageBox.Show("A waste record for this item already exists today.");
                return;
            }

            var newWasteRecord = new Classes.WasteRecord
            {
                ItemID = selectedItemId,
                Date = DateTime.Now,
                WasteType = WasteTypeTextBox.Text,
                Quantity = int.Parse(QuantityTextBox.Text),
                InventoryRecordID = existingInventoryRecordForToday.RecordID,
                //InventoryRecord = existingInventoryRecordForToday // Explicitly adding the InventoryRecord
            };

            var response = await _apiService.AddWasteRecordAsync(newWasteRecord);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Waste record added successfully.");
                Close();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Failed to add waste record. Error: {response.StatusCode} - {errorContent}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
