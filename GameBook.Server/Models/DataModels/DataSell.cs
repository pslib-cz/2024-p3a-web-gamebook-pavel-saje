using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataSell
    {
        [Key]
        public int SellID { get; set; }
        public int interactibleID { get; set; }
        public int itemID { get; set; }
        [ForeignKey(nameof(interactibleID))]
        public DataInteractible? Interactible { get; set; } = null!;
        [ForeignKey(nameof(itemID))]
        public DataItem? Item { get; set; } = null!;
    }
}
