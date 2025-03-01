namespace GameBook.Server.Models
{
    public class InputItem
    {
        public string Name { get; set; }
        public int? TradeValue { get; set; }
        public bool Stackable { get; set; }
        public int RadiationGain { get; set; }
        public int CategoryId { get; set; }
    }
}
