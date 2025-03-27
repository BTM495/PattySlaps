using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PattySlapsApp
{
    public class InventoryRecord : INotifyPropertyChanged
    {
        public int RecordID { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int SoDQuantity { get; set; } // Start of Day Quantity
        public int EoDQuantity { get; set; } // End of Day Quantity
        public int QuantityUsed { get; set; } // Quantity used during the day
        public bool DiscrepancyFlag { get; set; } // Flag if SoD - QuantityUsed != EoD
        [ForeignKey("Item")]
        public int ItemID { get; set; }  // Foreign key reference to Item

        private Item _item;
        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
                OnPropertyChanged(nameof(ItemName));
            }
        }

        public string ItemName => Item?.Name;

        public List<WasteRecord> WasteRecords { get; set; } = new List<WasteRecord>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class WasteRecord : INotifyPropertyChanged
    {
        public int WasteID { get; set; }
        public int InventoryRecordID { get; set; }
        public string WasteType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Item")]
        public int ItemID { get; set; }  // Foreign key reference to Item

        private Item _item;
        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
                OnPropertyChanged(nameof(ItemName));
            }
        }

        public string ItemName => Item?.Name;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string CountType { get; set; } // Example: "Units", "Kg", "L"
        public decimal Price { get; set; } // Price per unit
        public int StockQuantity { get; set; } // Current stock level
    }
}
