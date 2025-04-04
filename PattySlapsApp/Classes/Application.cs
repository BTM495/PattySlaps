using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PattySlapsApp.Classes
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }

        [ForeignKey("Applicant")]
        public int ApplicantID { get; set; }

        [ForeignKey("Position")]
        public int PositionID { get; set; }

        public string Status { get; set; }

        public DateTime SubmissionDate { get; set; }
    }
}
