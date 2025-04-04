using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PattySlapsApp.Classes
{
    public class Applicant
    {
        [Key]
        public int ApplicantID { get; set; }
        public string PersonalInfo { get; set; }
        public string EducationLevel { get; set; }
        public string Experience { get; set; }
        public string Availability { get; set; }
        public string HourPreferences { get; set; }
        public string Resume { get; set; }
    }
}
