using System;

namespace Milestone_2.Classes
{
    class Supervisor : Employee
    {
        public int SupervisorID { get; set; }

        public void AddSupervisor()
        {
            SupervisorID = new Random().Next(1000, 9999);
            Console.WriteLine("Supervisor added successfully.");
        }

        public void DeleteSupervisor()
        {
            Console.WriteLine($"Supervisor deleted successfully. ID: {SupervisorID}");
            SupervisorID = 0;
        }

        public void CheckQC(int supervisorID, string password)
        {
            if (SupervisorID == supervisorID && Password == password)
            {
                Console.WriteLine("QC checked successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Supervisor ID or Password.");
            }
        }
    }
}
