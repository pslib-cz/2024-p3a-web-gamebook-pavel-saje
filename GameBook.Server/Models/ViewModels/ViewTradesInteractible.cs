namespace GameBook.Server.Models
{
    public class ViewTradesInteractible
    {
        public int TradesInteractibleID { get; set; }
        public int InteractibleID { get; set; }
        public int TradeInteractibleID { get; set; }
        public ViewInteractible Interactible { get; set; }
        public ViewTradeInteractible TradeInteractible { get; set; }
    }
}
