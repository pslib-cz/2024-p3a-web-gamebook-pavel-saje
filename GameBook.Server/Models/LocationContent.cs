using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class LocationContent
    {
        [Key]
        public int LocationContentID { get; set; }
        public int LocationID { get; set; }
        public int InteractibleID { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }

        [ForeignKey(nameof(LocationID))]
        public Location Location { get; set; }

        [ForeignKey(nameof(InteractibleID))]
        public Interactible Interactible { get; set; }
    }
}
