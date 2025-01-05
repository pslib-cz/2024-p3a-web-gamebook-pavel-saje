using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class ConsumableItem
    {
        [Key]
        public int ConsumableItemID { get; set; }
        public int ItemID { get; set; }
        public int HealthValue { get; set; }
        public int EnergyValue { get; set; }
        public int RadiationValue { get; set; }

        [ForeignKey(nameof(ItemID))]
        public Item Item { get; set; }
    }
}
