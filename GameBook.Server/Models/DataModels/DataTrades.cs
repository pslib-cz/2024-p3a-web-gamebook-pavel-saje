using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataTrades
    {
        [Key]
        public int TradesID { get; set; }
        public int interactibleID { get; set; }
        public int tradeID { get; set; }

        [ForeignKey(nameof(interactibleID))]
        public DataInteractible interactible { get; set; }
        [ForeignKey(nameof(tradeID))]
        public DataTrade trade { get; set; }
    }
}
