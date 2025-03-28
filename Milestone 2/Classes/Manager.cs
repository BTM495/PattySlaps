using System;

namespace Milestone_2.Classes
{
    class Manager : Employee
    {
        public int ManagerID { get; set; }
        public string Location { get; set; }

        public void AddManager(string location)
        {
            ManagerID = new Random().Next(1000, 9999);
            Location = location;
            Console.WriteLine("Manager added successfully.");
        }

        public void DeleteManager()
        {
            Console.WriteLine($"Manager deleted successfully. ID: {ManagerID}");
            ManagerID = 0;
            Location = null;
        }
    }
}
