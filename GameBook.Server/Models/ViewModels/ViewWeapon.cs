namespace GameBook.Server.Models
{
    public class ViewWeapon
    {
        public int WeaponID { get; set; }
        public int ItemID { get; set; }
        public string Name { get; set; } = null!;
        public int Damage { get; set; }
        public ViewItem Item { get; set; } = null!;
    }
}
