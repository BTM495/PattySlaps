using System;

namespace Milestone_2.Classes
{
    class EndOfShiftReport
    {
        public int ReportID { get; set; }
        public int SupervisorID { get; set; }
        public string Shift { get; set; }
        public string Status { get; set; }
        public string SummaryOfOps { get; set; }
        public string ChallengesEncountered { get; set; }
        public string MaterialsOutOfStock { get; set; }
        public string UnresolvedIssues { get; set; }
        public string? Incidents { get; set; }
        public string? Warnings { get; set; }
        public string Notes { get; set; }

        public void CreateEndOfShiftReport(int supervisorID, string shift, string status, string summaryOfOps, string challengesEncountered, string materialsOutOfStock, string unresolvedIssues, string incidents, string warnings, string notes)
        {
            ReportID = new Random().Next(1000, 9999);
            SupervisorID = supervisorID;
            Shift = shift;
            Status = status;
            SummaryOfOps = summaryOfOps;
            ChallengesEncountered = challengesEncountered;
            MaterialsOutOfStock = materialsOutOfStock;
            UnresolvedIssues = unresolvedIssues;
            Incidents = incidents;
            Warnings = warnings;
            Notes = notes;
            Console.WriteLine("End of shift report created successfully.");
        }

        public void UpdateEndOfShiftReport(string status, string summaryOfOps, string challengesEncountered, string materialsOutOfStock, string unresolvedIssues, string incidents, string warnings, string notes)
        {
            Status = status;
            SummaryOfOps = summaryOfOps;
            ChallengesEncountered = challengesEncountered;
            MaterialsOutOfStock = materialsOutOfStock;
            UnresolvedIssues = unresolvedIssues;
            Incidents = incidents;
            Warnings = warnings;
            Notes = notes;
            Console.WriteLine("End of shift report updated successfully.");
        }

        public string AccessEndofShiftReport()
        {
            return $"Report ID: {ReportID}, Supervisor ID: {SupervisorID}, Shift: {Shift}, Status: {Status}, Summary of Operations: {SummaryOfOps}, Challenges Encountered: {ChallengesEncountered}, Materials Out of Stock: {MaterialsOutOfStock}, Unresolved Issues: {UnresolvedIssues}, Incidents: {Incidents}, Warnings: {Warnings}, Notes: {Notes}";
        }
    }
}
