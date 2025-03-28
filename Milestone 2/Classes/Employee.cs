using System;

namespace Milestone_2.Classes
{
    class Employee
    {
        public int EmployeeID { get; set; }
        public string Password { get; set; }
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
        public string Availability { get; set; }

        public void AddEmployee(string password, string firstName, string lastName, string sin, string phoneNumber, string email, string address, DateTime birthDate, string role, string employmentStatus, string employmentType, decimal wage, string availability)
        {
            EmployeeID = new Random().Next(1000, 9999);
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            SIN = sin;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            BirthDate = birthDate;
            Role = role;
            EmploymentStatus = employmentStatus;
            EmploymentType = employmentType;
            Wage = wage;
            Availability = availability;
            Console.WriteLine("Employee added successfully.");
        }

        public void UpdateEmployee(string password, string firstName, string lastName, string phoneNumber, string email, string address, string role, string employmentStatus, string employmentType, decimal wage, string availability)
        {
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            Role = role;
            EmploymentStatus = employmentStatus;
            EmploymentType = employmentType;
            Wage = wage;
            Availability = availability;
            Console.WriteLine("Employee updated successfully.");
        }

        public void DeleteEmployee()
        {
            Console.WriteLine($"Employee deleted successfully. ID: {EmployeeID}");
            EmployeeID = 0;
            Password = null;
            FirstName = null;
            LastName = null;
            SIN = null;
            PhoneNumber = null;
            Email = null;
            Address = null;
            BirthDate = DateTime.MinValue;
            Role = null;
            EmploymentStatus = null;
            EmploymentType = null;
            Wage = 0;
            Availability = null;
        }
    }
}
