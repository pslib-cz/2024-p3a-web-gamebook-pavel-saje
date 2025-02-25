using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataTradeInteractible
    {
        [Key]
        public int TradeInteractibleID { get; set; }
        public int InteractibleID { get; set; }
        public int ItemID { get; set; }
        public int TradeValue { get; set; }
        public string Text { get; set; }
        [ForeignKey(nameof(ItemID))]
        public DataItem Item { get; set; }
        [ForeignKey(nameof(InteractibleID))]
        public DataInteractible Interactible { get; set; }
    }
}
