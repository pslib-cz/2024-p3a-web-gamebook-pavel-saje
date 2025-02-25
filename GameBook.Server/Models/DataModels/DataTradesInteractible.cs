using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataTradesInteractible
    {
        [Key]
        public int TradesInteractibleID { get; set; }
        public int InteractibleID { get; set; }
        public int TradeInteractibleID { get; set; }

        [ForeignKey(nameof(InteractibleID))]
        public DataInteractible Interactible { get; set; }
        [ForeignKey(nameof(TradeInteractibleID))]
        public DataTradeInteractible TradeInteractible { get; set; }

    }
}
