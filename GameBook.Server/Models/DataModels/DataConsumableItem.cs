using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataConsumableItem
    {
        [Key]
        public int ConsumableItemID { get; set; }
        public int ItemID { get; set; }
        public int HealthValue { get; set; }
        public int EnergyValue { get; set; }
        public int RadiationValue { get; set; }

        [ForeignKey(nameof(ItemID))]
        public DataItem Item { get; set; }
    }
}
