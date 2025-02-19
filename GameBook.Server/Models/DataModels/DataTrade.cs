using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataTrade
    {
        [Key]
        public int TradeID { get; set; }
        public int Item1ID { get; set; }
        public int Item2ID { get; set; }

        [ForeignKey(nameof(Item1ID))]
        public DataItem Item1 { get; set; }
        [ForeignKey(nameof(Item2ID))]
        public DataItem Item2 { get; set; }
    }
}
