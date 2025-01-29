using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class ViewConsumableItem
    {
        public int ConsumableItemID { get; set; }
        public int ItemID { get; set; }
        public int HealthValue { get; set; }
        public int EnergyValue { get; set; }
        public int RadiationValue { get; set; }
        public ViewItem Item { get; set; }
    }
}
