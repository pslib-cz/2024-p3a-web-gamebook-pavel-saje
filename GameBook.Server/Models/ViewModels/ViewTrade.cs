namespace GameBook.Server.Models
{
    public class ViewTrade
    {
        public int TradeID { get; set; }
        public int Item1ID { get; set; }
        public int Item2ID { get; set; }
        public ViewItem Item1 { get; set; }
        public ViewItem Item2 { get; set; }
    }
}
