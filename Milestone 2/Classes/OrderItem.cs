using System;

namespace Milestone_2.Classes
{
    class OrderItem
    {
        public int OrderItemID { get; set; }
        public string OrderItemName { get; set; }
        public decimal Price { get; set; }

        public void AddOrderItem(string orderItemName, decimal price)
        {
            OrderItemID = new Random().Next(1000, 9999);
            OrderItemName = orderItemName;
            Price = price;
            Console.WriteLine($"Order item added successfully. ID: {OrderItemID}, Name: {OrderItemName}, Price: {Price:C}");
        }

        public void EditOrderItem(string newName, decimal newPrice)
        {
            OrderItemName = newName;
            Price = newPrice;
            Console.WriteLine($"Order item edited successfully. New Name: {OrderItemName}, New Price: {Price:C}");
        }

        public void DeleteOrderItem()
        {
            Console.WriteLine($"Order item deleted successfully. ID: {OrderItemID}");
            OrderItemID = 0;
            OrderItemName = null;
            Price = 0;
        }
    }
}
