using System;

namespace Milestone_2.Classes
{
    class InventoryRecord
    {
        public int InventoryRecordID { get; set; }
        public int? ItemID { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int SODQuantity { get; set; }
        public int EODQuantity { get; set; }
        public bool DiscrepancyFlag { get; set; }

        public void CreateInventoryRecord(int itemID, int quantity, int sodQuantity, int eodQuantity)
        {
            InventoryRecordID = new Random().Next(1000, 9999);
            ItemID = itemID;
            Date = DateTime.Now;
            Quantity = quantity;
            SODQuantity = sodQuantity;
            EODQuantity = eodQuantity;
            DiscrepancyFlag = (sodQuantity != eodQuantity);
            Console.WriteLine("Inventory record created successfully.");
        }

        public void UpdateInventoryRecord(int quantity, int sodQuantity, int eodQuantity)
        {
            Quantity = quantity;
            SODQuantity = sodQuantity;
            EODQuantity = eodQuantity;
            DiscrepancyFlag = (sodQuantity != eodQuantity);
            Console.WriteLine("Inventory record updated successfully.");
        }

        public void DeleteInventoryRecord()
        {
            Console.WriteLine($"Inventory record deleted successfully. ID: {InventoryRecordID}");
            InventoryRecordID = 0;
            ItemID = null;
            Quantity = 0;
            SODQuantity = 0;
            EODQuantity = 0;
            DiscrepancyFlag = false;
        }
    }
}
