
namespace GameBook.Server.Models
{
    public class ViewNpc
    {
        public int NpcID { get; set; }
        public string? Name { get; set; }
        public int? Health { get; set; }
        public int? WeaponID { get; set; }
        public ViewWeapon? Weapon { get; set; }

        public static implicit operator ViewNpc(DataInteractiblesNpc v)
        {
            throw new NotImplementedException();
        }
    }
}
