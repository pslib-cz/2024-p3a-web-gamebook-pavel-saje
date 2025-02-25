namespace GameBook.Server.Models
{
    public class ViewTradeInteractible
    {
        public int TradeInteractibleID { get; set; }
        public int InteractibleID { get; set; }
        public int ItemID { get; set; }
        public string Text { get; set; }
        public ViewItem Item { get; set; }
        public ViewInteractible Interactible { get; set; }
    }
}
