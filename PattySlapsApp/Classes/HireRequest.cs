using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PattySlapsApp.Classes
{
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
}
