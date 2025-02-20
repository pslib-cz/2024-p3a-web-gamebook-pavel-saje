using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataEnd
    {
        [Key]
        public int EndID { get; set; }
        public int LocationID { get; set; }
        public int DialogID { get; set; }

        [ForeignKey(nameof(LocationID))]
        public DataLocation Location { get; set; }

        [ForeignKey(nameof(DialogID))]
        public DataDialog Dialog { get; set; }
    }
}