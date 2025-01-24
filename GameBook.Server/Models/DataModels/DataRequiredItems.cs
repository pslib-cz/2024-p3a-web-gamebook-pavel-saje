using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataRequiredItems
    {
        public int LocationID { get; set; }
        [ForeignKey(nameof(LocationID))]
        public DataLocation Location { get; set; }
        public int ItemID { get; set; }
        [ForeignKey(nameof(ItemID))]
        public DataItem Item { get; set; }
    }
}