namespace GameBook.Server.Models
{
    public class ViewBuy
    {
        public int BuyID { get; set; }
        public int interactibleID { get; set; }
        public int itemID { get; set; }
        public ViewInteractible Interactible { get; set; }
        public ViewItem Item { get; set; }
    }
}
