using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PattySlapsApp.Classes
{
    public class QCChecklist
    {
        [Key]
        public int? QCID { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Item")]
        public int? ItemID { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDefect { get; set; }
        public int? Quantity { get; set; }
        public string? ItemPicture { get; set; }
        public bool? Completed { get; set; }
    }
}
