using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace PattySlaps
{
    // Class for Kiosk
    public class Kiosk
    {
        public int KioskID { get; set; }  // Unique identifier for the kiosk
        public string Location { get; set; }  // Location of the kiosk (e.g., in the store)
        public bool IsActive { get; private set; } = true; // Kiosk status

        public void ActivateKiosk() => IsActive = true;
        public void DeactivateKiosk() => IsActive = false;
    }

    // Class for Branch (One branch has many employees)

    public class Branch
    {
        [Key]
        public int BranchID { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        // One Branch has Many Employees
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public void AddEmployee(Employee employee) => Employees.Add(employee);
        public void RemoveEmployee(Employee employee) => Employees.Remove(employee);
    }

    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }
    }

    // Order class (One order has many items)
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderType { get; set; }
        public decimal OrderTotal { get; set; }
        public string Status { get; set; }

        // Many-to-Many Relationship: OrderItems as a Join Table
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        /*public void CalculateTotal()
        {
            OrderTotal = 0;
            foreach (var item in OrderItems)
            {
                OrderTotal += item.Price;
            }
        }*/

        public void UpdateStatus(string newStatus) => Status = newStatus;
    }

    // HRMS class
    public class HRMS
    {
        [NotMapped]
        public List<int> JobPostings { get; set; } = new List<int>();
        public string JobPostingsJson
        {
            get => JsonSerializer.Serialize(JobPostings);
            set => JobPostings = string.IsNullOrEmpty(value) ? new List<int>() : JsonSerializer.Deserialize<List<int>>(value);
        }

        [NotMapped]
        public List<int> Applications { get; set; } = new List<int>();
        public string ApplicationsJson
        {
            get => JsonSerializer.Serialize(Applications);
            set => Applications = string.IsNullOrEmpty(value) ? new List<int>() : JsonSerializer.Deserialize<List<int>>(value);
        }

        [NotMapped]
        public List<string> UserAccessLevels { get; set; } = new List<string>();
        public string UserAccessLevelsJson
        {
            get => JsonSerializer.Serialize(UserAccessLevels);
            set => UserAccessLevels = string.IsNullOrEmpty(value) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(value);
        }

        [NotMapped]
        public List<string> EmailTemplates { get; set; } = new List<string>();
        public string EmailTemplatesJson
        {
            get => JsonSerializer.Serialize(EmailTemplates);
            set => EmailTemplates = string.IsNullOrEmpty(value) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(value);
        }
    }

    // Applicant class
    public class Applicant
    {
        [Key]
        public int ApplicantID { get; set; }
        public string PersonalInfo { get; set; }
        public string EducationLevel { get; set; }
        public string Experience { get; set; }
        public string Availability { get; set; }
        public string HourPreferences { get; set; }
        public List<Application> Applications { get; set; } = new List<Application>();
    }

    // Application class
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }
        [ForeignKey("Applicant")]
        public int ApplicantID { get; set; }
        [ForeignKey("Position")]
        public int PositionID { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }

        public void ApproveApplication() => Status = "Approved";
        public void RejectApplication() => Status = "Rejected";
    }

    // Position class
    public class Position
    {
        [Key]
        public int PositionID { get; set; }
        public string Title { get; set; }
        public string Requirements { get; set; }
        public string Status { get; set; }
        public List<Application> Applications { get; set; } = new List<Application>();
    }

    // Hire Request class
    public class HireRequest
    {
        [Key]
        public int RequestID { get; set; }
        public DateTime Date { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public DateTime StartingDate { get; set; }
        public string RequestingManager { get; set; }
    }

    public class ShiftScheduleEmployee
    {
        [Key]
        public int ShiftScheduleEmployeeID { get; set; }

        [ForeignKey("ShiftSchedule")]
        public int ScheduleID { get; set; }
        public ShiftSchedule ShiftSchedule { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }

    // Shift Schedule class (One shift can have multiple employees)
    public class ShiftSchedule
    {
        [Key]
        public int ScheduleID { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Status { get; set; }
        public string ConflictAlerts { get; set; }

        // Many-to-Many Relationship
        public List<ShiftScheduleEmployee> ShiftScheduleEmployees { get; set; } = new List<ShiftScheduleEmployee>();
    }


    // Summary Report class
    public class SummaryReport
    {
        [Key]
        public int ReportID { get; set; }
        public string TotalWaste { get; set; }
        public string DiscrepancyDetails { get; set; }
    }

    // Inventory Record class
    public class InventoryRecord
    {
        [Key]
        public int RecordID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public int SoDQuantity { get; set; } // Start-of-Day Quantity

        [Required]
        public int EoDQuantity { get; set; } // End-of-Day Quantity

        [Required]
        public int QuantityUsed { get; set; }

        public bool DiscrepancyFlag { get; set; }

        // Foreign key to associate with an Item
        [ForeignKey("Item")]
        public int? ItemID { get; set; }
        public Item? Item { get; set; }

        // Relationship with Waste Records
        public List<WasteRecord> WasteRecords { get; set; } = new List<WasteRecord>();
    }

    public class InventoryItemEntry
    {
        [Key]
        public int EntryID { get; set; }

        [ForeignKey("InventoryRecord")]
        public int RecordID { get; set; }
        public InventoryRecord InventoryRecord { get; set; }

        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public Item Item { get; set; }

        public int SoDQuantity { get; set; } // Start-of-Day Quantity
        public int QuantityUsed { get; set; } // How much was used
        public int EoDQuantity { get; set; } // End-of-Day Quantity
        public bool DiscrepancyFlag { get; set; } // If there's a mismatch

        public void UpdateStock(int newEoDQuantity)
        {
            EoDQuantity = newEoDQuantity;
            DiscrepancyFlag = (SoDQuantity - QuantityUsed) != EoDQuantity;
        }
    }


    // Item class
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string CountType { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; } // Added stock quantity attribute
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public bool CheckStockLevel(int threshold) => StockQuantity >= threshold;
    }

    // Waste Record class
    public class WasteRecord
    {
        [Key]
        public int WasteID { get; set; }

        [ForeignKey("InventoryRecord")]
        public int InventoryRecordID { get; set; }
        public InventoryRecord InventoryRecord { get; set; }

        public string WasteType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        // Foreign key to associate with an Item
        [ForeignKey("Item")]
        public int? ItemID { get; set; }
        public Item? Item { get; set; }
    }

    // Reception QC Checklist class
    public class ReceptionQCChecklist
    {
        [Key]
        public int QCID { get; set; }
        public DateTime Date { get; set; }
        public string ItemName { get; set; }
        public string ItemDefect { get; set; }
        public int Quantity { get; set; }
        public string ItemPicture { get; set; }
    }

    // End-of-Shift Report class
    public class EndOfShiftReport
    {
        [Key]
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

    // Employee class (Many-to-Many with ShiftSchedule)
    public class Employee
    {
        [Key]
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
        [ForeignKey("Branch")]
        public int? BranchID { get; set; }
        // Many-to-Many Relationship
        public List<ShiftScheduleEmployee> ShiftScheduleEmployees { get; set; } = new List<ShiftScheduleEmployee>(); 
        public void UpdateRole(string newRole) => Role = newRole;
        public void UpdateWage(decimal newWage) => Wage = newWage;
    }

    // Manager class inheriting Employee
    public class Manager : Employee
    {
        public int ManagerID { get; set; }
    }

    // Team Member class inheriting Employee
    public class TeamMember : Employee
    {
        public string Availability { get; set; }
    }

    // Supervisor class inheriting Employee
    public class Supervisor : Employee
    {
        public int SupervisorID { get; set; }
        public List<ShiftSchedule> ShiftSchedules { get; set; } = new List<ShiftSchedule>(); // One-to-Many
    }

    // HR Employee class inheriting Employee
    public class HREmployee : Employee
    {
        public string Department { get; set; }
        /*[NotMapped] // Prevents EF Core from mapping List<int> directly
         public List<int> ManagedPositions { get; set; } = new List<int>();

         // Store the List<int> as a JSON string in the database
         public string ManagedPositionsJsons
         {
             get => JsonSerializer.Serialize(ManagedPositions);
             set => ManagedPositions = string.IsNullOrEmpty(value) ? new List<int>() : JsonSerializer.Deserialize<List<int>>(value);
         }
     }*/
    }
}