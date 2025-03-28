using System;

namespace Milestone_2.Classes
{
    class HireRequest
    {
        public int RequestID { get; set; }
        public int ManagerID { get; set; }
        public DateTime Date { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public DateTime StartingDate { get; set; }
        public string RequestingManager { get; set; }

        public void CreateHireRequest(int managerID, string position, DateTime startingDate, string requestingManager)
        {
            RequestID = new Random().Next(1000, 9999);
            ManagerID = managerID;
            Date = DateTime.Now;
            Position = position;
            Status = "Created";
            StartingDate = startingDate;
            RequestingManager = requestingManager;
            Console.WriteLine("Hire request created successfully.");
        }

        public void UpdateHireRequest(string status)
        {
            Status = status;
            Console.WriteLine("Hire request updated successfully.");
        }

        public void DeleteHireRequest()
        {
            Console.WriteLine($"Hire request deleted successfully. ID: {RequestID}");
            RequestID = 0;
            ManagerID = 0;
            Position = null;
            Status = null;
            StartingDate = DateTime.MinValue;
            RequestingManager = null;
        }
    }
}
