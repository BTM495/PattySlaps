using System;

namespace Milestone_2.Classes
{
    class ShiftSchedule
    {
        public int ScheduleID { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public List<int> EmployeeID { get; set; }
        public string Status { get; set; }
        public string ConflictAlerts { get; set; }
        public List<int> BackupEmployeeID { get; set; }
        public string BranchName { get; set; }

        public void CreateSchedule(DateTime date, string shift, List<int> employeeID, string status, string branchName, List<int> backupEmployeeID)
        {
            ScheduleID = new Random().Next(1000, 9999);
            Date = date;
            Shift = shift;
            List<int> EmployeeID = employeeID;
            List<int> BackupEmployeeID = backupEmployeeID;
            Status = status;
            BranchName = branchName;
            Console.WriteLine("Shift schedule created successfully.");
        }

        public void UpdateSchedule(DateTime date, string shift, List<int> employeeID, string status, string branchName, List<int> backupEmployeeID)
        {
            Date = date;
            Shift = shift;
            List<int> EmployeeID = employeeID;
            List<int> BackupEmployeeID = backupEmployeeID;
            Status = status;
            BranchName = branchName;
            Console.WriteLine("Shift schedule updated successfully.");
        }

        public void DeleteSchedule()
        {
            Console.WriteLine($"Shift schedule deleted successfully. ID: {ScheduleID}");
            ScheduleID = 0;
            Date = DateTime.MinValue;
            Shift = null;
            EmployeeID = null;
            BackupEmployeeID = null;
            Status = null;
            BranchName = null;
        }
    }
}
