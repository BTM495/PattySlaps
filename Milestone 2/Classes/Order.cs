using System;

namespace Milestone_2.Classes
{
    class Order
    {
        public int OrderID { get; set; }
        public int KioskID { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public string PaymentMethod { get; set; }
        public string OrderType { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderTime { get; set; }
        public string BranchName { get; set; }

        public void CreateOrder(int kioskID, string customerName, string paymentMethod, string orderType, string branchName)
        {
            OrderID = new Random().Next(1000, 9999);
            KioskID = kioskID;
            CustomerName = customerName;
            PaymentMethod = paymentMethod;
            OrderType = orderType;
            BranchName = branchName;
            OrderTime = DateTime.Now;
            OrderStatus = "Created";
            Console.WriteLine("Order created successfully.");
        }

        public void CalculateTotal()
        {
            OrderTotal = 0;
            foreach (var item in OrderItems)
            {
                OrderTotal += item.Price;
            }
            Console.WriteLine($"Order total calculated: {OrderTotal:C}");
        }

        public void UpdateOrder(List<OrderItem> newOrderItems)
        {
            OrderItems = newOrderItems;
            CalculateTotal();
            Console.WriteLine("Order updated successfully.");
        }

        public void ProcessPayment()
        {
            if (OrderStatus == "Created")
            {
                OrderStatus = "Paid";
                Console.WriteLine("Payment processed successfully.");
            }
            else
            {
                Console.WriteLine("Order is not in a state to process payment.");
            }
        }

        public void CancelOrder()
        {
            if (OrderStatus == "Created")
            {
                OrderStatus = "Canceled";
                Console.WriteLine("Order canceled successfully.");
            }
            else
            {
                Console.WriteLine("Order cannot be canceled.");
            }
        }

        public void PrintReceipt()
        {
            Console.WriteLine("Printing receipt...");
            Console.WriteLine($"Order ID: {OrderID}");
            Console.WriteLine($"Customer Name: {CustomerName}");
            Console.WriteLine($"Order Items: {string.Join(", ", OrderItems)}");
            Console.WriteLine($"Order Total: {OrderTotal:C}");
            Console.WriteLine($"Order Status: {OrderStatus}");
            Console.WriteLine($"Order Time: {OrderTime}");
            Console.WriteLine($"Branch Name: {BranchName}");
        }
    }
}
