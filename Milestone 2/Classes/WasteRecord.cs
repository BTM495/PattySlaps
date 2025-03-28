using System;

namespace Milestone_2.Classes
{
    class WasteRecord
    {
        public int WasteRecordID { get; set; }
        public int InventoryRecordID { get; set; }
        public string ItemID { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string WasteCategory { get; set; }
        public DateTime Time { get; set; }

        public void CreateWasteRecord(int inventoryRecordID, string itemID, int quantity, string wasteCategory)
        {
            WasteRecordID = new Random().Next(1000, 9999);
            InventoryRecordID = inventoryRecordID;
            ItemID = itemID;
            Quantity = quantity;
            Date = DateTime.Now;
            WasteCategory = wasteCategory;
            Time = DateTime.Now;
            Console.WriteLine("Waste record created successfully.");
        }

        public void UpdateWasteRecord(int quantity, string wasteCategory)
        {
            Quantity = quantity;
            WasteCategory = wasteCategory;
            Console.WriteLine("Waste record updated successfully.");
        }

        public void DeleteWasteRecord()
        {
            Console.WriteLine($"Waste record deleted successfully. ID: {WasteRecordID}");
            WasteRecordID = 0;
            InventoryRecordID = 0;
            ItemID = null;
            Quantity = 0;
            WasteCategory = null;
            Time = DateTime.MinValue;
        }
    }
}
