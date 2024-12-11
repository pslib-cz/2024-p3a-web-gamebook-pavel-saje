using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class Locations
    {
        [Key]
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string BackgroundImage { get; set; }
        public int RadiationGain { get; set; }
    }

    public class LocationContent
    {
        [Key]
        public int LocationContentID { get; set; }

        [ForeignKey(nameof(LocationID))]
        public int LocationID { get; set; }
        public int InteractibleID { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
    }

    public class LocationPaths
    {
        [Key]
        public int PathID { get; set; }
        public int FirstNodeID { get; set; }
        public int SecondNodeID { get; set; }
        public int EnergyTravelCost { get; set; }
        }
}
