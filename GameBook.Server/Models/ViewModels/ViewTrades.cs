namespace GameBook.Server.Models
{
    public class ViewTrades
    {
        public int TradesID { get; set; }
        public int interactibleID { get; set; }
        public int tradeID { get; set; }
        public ViewInteractible interactible { get; set; }
        public ViewTrade trade { get; set; }
    }
}
