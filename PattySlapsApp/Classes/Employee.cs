using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PattySlapsApp.Classes
{
    public class Employee
    {
        public int EmployeeID { get; set; }  // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SIN { get; set; }  // Social Insurance Number
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }  // Nullable DateTime
        public string Role { get; set; }
        public string EmploymentStatus { get; set; }  // Active, Terminated, etc.
        public string EmploymentType { get; set; }  // Full-time, Part-time, etc.
        public decimal Wage { get; set; }
        public int? BranchID { get; set; }  // Nullable in case an employee is not assigned to a branch
    }

}
