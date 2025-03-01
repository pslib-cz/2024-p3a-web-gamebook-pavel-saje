using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class InputConsumableItem
    {
        [Required]
        public int ItemID { get; set; }
        [Required]
        public int HealthValue { get; set; }
        [Required]
        public int EnergyValue { get; set; }
        [Required]
        public int RadiationValue { get; set; }
    }
}
