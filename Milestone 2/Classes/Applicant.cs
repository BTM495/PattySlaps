using System;

namespace Milestone_2.Classes
{
    class Applicant
    {
        public int ApplicantID { get; set; }
        public string PersonalInfo { get; set; }
        public string Experience { get; set; }
        public string EducationLevel { get; set; }
        public string Resume { get; set; }
        public string CoverLetter { get; set; }
        public string Availability { get; set; }
        public string HourPreference { get; set; }

        public void CreateApplicant(string personalInfo, string educationLevel, string resume, string coverLetter, string availability, string hourPreference, string experience)
        {
            ApplicantID = new Random().Next(1000, 9999);
            PersonalInfo = personalInfo;
            EducationLevel = educationLevel;
            Resume = resume;
            CoverLetter = coverLetter;
            Availability = availability;
            HourPreference = hourPreference;
            Experience = experience;
            Console.WriteLine("Applicant created successfully.");
        }

        public void UpdateApplicant(string personalInfo, string educationLevel, string resume, string coverLetter, string availability, string hourPreference, string experience)
        {
            PersonalInfo = personalInfo;
            EducationLevel = educationLevel;
            Resume = resume;
            CoverLetter = coverLetter;
            Availability = availability;
            HourPreference = hourPreference;
            Experience = experience;
            Console.WriteLine("Applicant updated successfully.");
        }

        public void DeleteApplicant()
        {
            Console.WriteLine($"Applicant deleted successfully. ID: {ApplicantID}");
            ApplicantID = 0;
            PersonalInfo = null;
            EducationLevel = null;
            Resume = null;
            CoverLetter = null;
            Availability = null;
            HourPreference = null;
            Experience = null;
        }
    }
}
