using System;
using System.Collections.Generic;

namespace PattySlaps
{
    // Class for Kiosk
    public class Kiosk
    {
        // Attributes for Kiosk
        public int KioskID { get; set; }  // Unique identifier for the kiosk
        public string Location { get; set; }  // Location of the kiosk (e.g., in the store)

        // Constructor to initialize the Kiosk
        public Kiosk(int kioskID, string location)
        {
            KioskID = kioskID;
            Location = location;
        }

        // Method to display Kiosk information
        public override string ToString()
        {
            return $"Kiosk {KioskID} located at {Location}";
        }
    }

    // Class for Order
    public class Order
    {
        public int Order_ID { get; set; }
        public string CustomerName { get; set; }
        public List<string> Items { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderType { get; set; }
        public decimal OrderTotal { get; set; }
        public string Status { get; set; }
    }

    // Class for Branch
    public class Branch
    {
        public int Branch_ID { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }

    // Class for HRMS
    public class HRMS
    {
        public void DisplayPositions() {}
        public void DisplayQuestionnaire() {}
        public void DisplayAvailabilityForm() {}
        public void DisplayPreferencesForm() {}
        public void CheckEligibility() {}
        public void StoreApplication() {}
        public void UpdateApplicationStatus() {}
        public void SendEmail() {}
    }

    // Class for Applicant
    public class Applicant
    {
        public string PersonalInfo { get; set; }
        public string EducationLevel { get; set; }
        public string Experience { get; set; }
        public string Availability { get; set; }
        public string HourPreferences { get; set; }
    }

    // Class for Application
    public class Application
    {
        public int ApplicationID { get; set; }
        public int ApplicantID { get; set; }
        public int PositionID { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
    }

    // Class for Position
    public class Position
    {
        public int PositionID { get; set; }
        public string Title { get; set; }
        public string Requirements { get; set; }
        public string Status { get; set; }
    }

    // Class for Hire Request
    public class HireRequest
    {
        public int RequestID { get; set; }
        public DateTime Date { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public DateTime StartingDate { get; set; }
        public string RequestingManager { get; set; }
    }

    // Class for Shift Schedule
    public class ShiftSchedule
    {
        public int ScheduleID { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Employees { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string ConflictAlerts { get; set; }
        public string BackupEmployees { get; set; }
    }

    // Class for Summary Report
    public class SummaryReport
    {
        public int ReportID { get; set; }
        public string TotalWaste { get; set; }
        public string DiscrepancyDetails { get; set; }
    }

    // Class for Inventory Record
    public class InventoryRecord
    {
        public int RecordID { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int SoDQuantity { get; set; }
        public int EoDQuantity { get; set; }
        public bool DiscrepancyFlag { get; set; }
    }

    // Class for Waste Record
    public class WasteRecord
    {
        public int RecordID { get; set; }
        public string WasteCategory { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
    }

    // Class for Item
    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string CountType { get; set; } // Weight/Units
    }

    // Class for Reception QC Checklist
    public class ReceptionQCChecklist
    {
        public int QCID { get; set; }
        public DateTime Date { get; set; }
        public string ItemName { get; set; }
        public string ItemDefect { get; set; }
        public int Quantity { get; set; }
        public string ItemPicture { get; set; }
    }

    // Class for End-of-Shift Report
    public class EndOfShiftReport
    {
        public int ReportID { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Status { get; set; }
        public string SummaryOfOps { get; set; }
        public string ChallengesEncountered { get; set; }
        public string MaterialsOutOfStock { get; set; }
        public string UnresolvedIssues { get; set; }
        public bool Incident { get; set; }
        public bool Warning { get; set; }
        public string Notes { get; set; }
    }

    // Employee Class
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SIN { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
        public string EmploymentStatus { get; set; }
        public string EmploymentType { get; set; }
        public decimal Wage { get; set; }
    }

    // Team Member Class
    public class TeamMember : Employee
    {
        public string Availability { get; set; }
    }

    // Supervisor Class
    public class Supervisor : Employee
    {
        public int SupervisorID { get; set; }
    }

    // Manager Class
    public class Manager : Employee
    {
        public int ManagerID { get; set; }
        public string Location { get; set; }
    }

    // HR Employee Class
    public class HREmployee : Employee
    {
}

