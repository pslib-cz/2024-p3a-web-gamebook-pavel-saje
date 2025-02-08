using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataWeapon
    {
        [Key]
        public int WeaponID { get; set; }
        public string Name { get; set; } = null!;
        public int Damage { get; set; }
    }
}
