using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataBuy
    {
        [Key]
        public int BuyID { get; set; }
        public int InteractibleID { get; set; }
        public int ItemID { get; set; }

        [ForeignKey(nameof(InteractibleID))]
        public DataInteractible Interactible { get; set; }
        [ForeignKey(nameof(ItemID))]
        public DataItem Item { get; set; }

    }
}
