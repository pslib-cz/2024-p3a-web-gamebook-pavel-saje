namespace GameBook.Server.Models
{
    public class ViewSell
    {
        public int SellID { get; set; }
        public int interactibleID { get; set; }
        public int itemID { get; set; }
        public ViewInteractible Interactible { get; set; } = null!;
        public ViewItem Item { get; set; } = null!;
    }
}
