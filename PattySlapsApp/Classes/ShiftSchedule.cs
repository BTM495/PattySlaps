using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PattySlapsApp.Classes
{
    public class ShiftSchedule
    {
        [Key]
        public int ScheduleID { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Status { get; set; }
    }

    public class ShiftScheduleEmployee
    {
        [Key]
        public int ShiftScheduleEmployeeID { get; set; }

        [ForeignKey("ShiftSchedule")]
        public int ScheduleID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
    }
}
