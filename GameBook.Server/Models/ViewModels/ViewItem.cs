namespace GameBook.Server.Models
{
    public class ViewItem
    {
        public int ItemID { get; set; }
        public required string Name { get; set; }
        public int? TradeValue { get; set; }
        public bool Stackable { get; set; }
        public int CategoryId { get; set; }
        public int RadiationGain { get; set; }
        public ViewItemCategory Category { get; set; }
    }
}
