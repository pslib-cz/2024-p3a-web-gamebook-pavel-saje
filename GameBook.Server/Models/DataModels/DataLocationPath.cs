using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataLocationPath
    {
        [Key]
        public int PathID { get; set; }
        public int FirstNodeID { get; set; }
        public int SecondNodeID { get; set; }
        public int EnergyTravelCost { get; set; }

        [ForeignKey(nameof(FirstNodeID))]
        public DataLocation FirstNode { get; set; }

        [ForeignKey(nameof(SecondNodeID))]
        public DataLocation SecondNode { get; set; }
    }
}
