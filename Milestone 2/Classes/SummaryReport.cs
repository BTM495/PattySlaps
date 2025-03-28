using System;
using System.Collections.Generic;

namespace Milestone_2.Classes
{
    class SummaryReport
    {
        public int SummaryReportID { get; set; }
        public List<int> InventoryRecordIDs { get; set; }
        public List<int> WasteRecordIDs { get; set; }
        public decimal TotalWaste { get; set; }
        public List<bool> DiscrepancyFlags { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndDate { get; set; }

        public void GenerateRecordSummary(List<int> inventoryRecordIDs, List<int> wasteRecordIDs, decimal totalWaste, List<bool> discrepancyFlags, DateTime beginningDate, DateTime endDate)
        {
            SummaryReportID = new Random().Next(1000, 9999);
            InventoryRecordIDs = inventoryRecordIDs;
            WasteRecordIDs = wasteRecordIDs;
            TotalWaste = totalWaste;
            DiscrepancyFlags = discrepancyFlags;
            BeginningDate = beginningDate;
            EndDate = endDate;
            Console.WriteLine("Record summary generated successfully.");
        }

        public void GenerateWasteSummary(decimal totalWaste)
        {
            TotalWaste = totalWaste;
            Console.WriteLine("Waste summary generated successfully.");
        }

        public string ViewReport()
        {
            string report = $"Summary Report ID: {SummaryReportID}\n" +
                            $"Inventory Record IDs: {string.Join(", ", InventoryRecordIDs)}\n" +
                            $"Waste Record IDs: {string.Join(", ", WasteRecordIDs)}\n" +
                            $"Total Waste: {TotalWaste:C}\n" +
                            $"Discrepancy Flags: {string.Join(", ", DiscrepancyFlags)}\n" +
                            $"Beginning Date: {BeginningDate}\n" +
                            $"End Date: {EndDate}\n";
            Console.WriteLine("Viewing report...");
            return report;
        }
    }
}
