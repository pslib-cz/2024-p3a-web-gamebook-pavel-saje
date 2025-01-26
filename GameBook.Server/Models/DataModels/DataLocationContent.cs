using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataLocationContent
    {
        [Key]
        public int LocationContentID { get; set; }
        public int LocationID { get; set; }
        [ForeignKey(nameof(LocationID))]
        public DataLocation Location { get; set; }
        public int InteractibleID { get; set; }
        [ForeignKey(nameof(InteractibleID))]
        public DataInteractible Interactible { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
    }
}
