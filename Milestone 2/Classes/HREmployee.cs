using System;
using System.Collections.Generic;

namespace Milestone_2.Classes
{
    class HREmployee : Employee
    {
        public int HREmployeeID { get; set; }
        public List<string> ManagedPositions { get; set; }
        public List<int> ProcessedApplications { get; set; }

        public void AssignHireRequest(int requestID, string position, int applicantID)
        {
            // Check if the position is managed by this HR employee
            if (ManagedPositions.Contains(position))
            {
                // Process the hire request
                ProcessedApplications.Add(applicantID);
                Console.WriteLine($"Hire request {requestID} for position {position} assigned to applicant {applicantID} successfully.");
            }
            else
            {
                Console.WriteLine($"Position {position} is not managed by this HR employee.");
            }
        }
    }
}
