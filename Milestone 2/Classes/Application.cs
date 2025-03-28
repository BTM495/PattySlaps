using System;

namespace Milestone_2.Classes
{
    class Application
    {
        public int ApplicationID { get; set; }
        public int ApplicantID { get; set; }
        public string PositionID { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }

        public void SubmitApplication(int applicantID, string positionID)
        {
            ApplicationID = new Random().Next(1000, 9999);
            ApplicantID = applicantID;
            PositionID = positionID;
            Status = "Submitted";
            SubmissionDate = DateTime.Now;
            Console.WriteLine("Application submitted successfully.");
        }

        public void UpdateApplication(string status)
        {
            Status = status;
            Console.WriteLine("Application updated successfully.");
        }
    }
}
