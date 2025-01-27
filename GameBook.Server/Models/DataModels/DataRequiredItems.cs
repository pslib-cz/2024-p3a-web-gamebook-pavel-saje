using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataRequiredItems
    {
        [Key]
        public int RequiredItemsID { get; set; }
        public int LocationID { get; set; }
        [ForeignKey(nameof(LocationID))]
        public DataLocation Location { get; set; }
        public int ItemID { get; set; }
        [ForeignKey(nameof(ItemID))]
        public DataItem Item { get; set; }
    }
}