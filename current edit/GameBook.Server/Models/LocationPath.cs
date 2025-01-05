using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class LocationPath
    {
        [Key]
        public int PathID { get; set; }
        public int FirstNodeID { get; set; }
        public int SecondNodeID { get; set; }
        public int EnergyTravelCost { get; set; }

        [ForeignKey(nameof(FirstNodeID))]
        public Location FirstNode { get; set; }

        [ForeignKey(nameof(SecondNodeID))]
        public Location SecondNode { get; set; }
    }
}
