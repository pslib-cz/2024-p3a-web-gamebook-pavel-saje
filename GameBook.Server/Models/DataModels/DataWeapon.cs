using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataWeapon
    {
        [Key]
        public int WeaponID { get; set; }
        public int? ItemID { get; set; }
        public string Name { get; set; } = null!;
        public int Damage { get; set; }

        [ForeignKey(nameof(ItemID))]
        public DataItem? Item { get; set; } = null!;
    }
}
