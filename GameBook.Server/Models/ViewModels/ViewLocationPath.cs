using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class ViewLocationPath
    {
        public int PathID { get; set; }
        public int FirstNodeID { get; set; }
        public int SecondNodeID { get; set; }
        public int EnergyTravelCost { get; set; }
        public ViewLocation FirstNode { get; set; }
        public ViewLocation SecondNode { get; set; }
    }
}
