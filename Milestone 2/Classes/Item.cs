using System;

namespace Milestone_2.Classes
{
    class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string CountType { get; set; }
        public DateTime ExpirationDate { get; set; }

        public void CreateItem(string name, string countType, DateTime expirationDate)
        {
            ItemID = new Random().Next(1000, 9999);
            Name = name;
            CountType = countType;
            ExpirationDate = expirationDate;
            Console.WriteLine("Item created successfully.");
        }

        public void UpdateItem(string name, string countType, DateTime expirationDate)
        {
            Name = name;
            CountType = countType;
            ExpirationDate = expirationDate;
            Console.WriteLine("Item updated successfully.");
        }

        public void DeleteItem()
        {
            Console.WriteLine($"Item deleted successfully. ID: {ItemID}");
            ItemID = 0;
            Name = null;
            CountType = null;
            ExpirationDate = DateTime.MinValue;
        }
    }
}
