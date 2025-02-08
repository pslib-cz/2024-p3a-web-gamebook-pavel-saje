using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataNpc
    {
        [Key]
        public int NpcID { get; set; }
        public string? Name { get; set; }
        public int? Health { get; set; }
        public int? WeaponID { get; set; }
        [ForeignKey(nameof(WeaponID))]
        public DataWeapon? Weapon { get; set; }

    }
}
