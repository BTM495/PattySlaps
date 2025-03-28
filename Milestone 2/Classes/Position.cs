using System;

namespace Milestone_2.Classes
{
    class Position
    {
        public int PositionID { get; set; }
        public string Title { get; set; }
        public string Requirements { get; set; }
        public string Status { get; set; }

        public void CheckEligibility()
        {
            // Example implementation for checking eligibility
            if (Status == "Open" && !string.IsNullOrEmpty(Requirements))
            {
                Console.WriteLine("Eligibility checked successfully. Position is open and has requirements.");
            }
            else
            {
                Console.WriteLine("Eligibility check failed. Position is either closed or has no requirements.");
            }
        }

        public void SelectPosition()
        {
            // Example implementation for selecting position
            if (PositionID != 0)
            {
                Console.WriteLine($"Position {Title} selected successfully. ID: {PositionID}");
            }
            else
            {
                Console.WriteLine("Position selection failed. No valid Position ID.");
            }
        }

        public void AddPosition(string title, string requirements, string status)
        {
            PositionID = new Random().Next(1000, 9999);
            Title = title;
            Requirements = requirements;
            Status = status;
            Console.WriteLine($"Position added successfully. ID: {PositionID}, Title: {Title}");
        }

        public void UpdatePosition(string title, string requirements, string status)
        {
            if (PositionID != 0)
            {
                Title = title;
                Requirements = requirements;
                Status = status;
                Console.WriteLine($"Position updated successfully. ID: {PositionID}, Title: {Title}");
            }
            else
            {
                Console.WriteLine("Position update failed. No valid Position ID.");
            }
        }

        public void DeletePosition()
        {
            if (PositionID != 0)
            {
                Console.WriteLine($"Position deleted successfully. ID: {PositionID}");
                PositionID = 0;
                Title = null;
                Requirements = null;
                Status = null;
            }
            else
            {
                Console.WriteLine("Position deletion failed. No valid Position ID.");
            }
        }

        public Position SearchPosition(string title)
        {
            // Example implementation for searching position by title
            if (Title != null && Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Position found: ID: {PositionID}, Title: {Title}");
                return this;
            }
            else
            {
                Console.WriteLine("No position found with the given title.");
                return null;
            }
        }
    }
}
