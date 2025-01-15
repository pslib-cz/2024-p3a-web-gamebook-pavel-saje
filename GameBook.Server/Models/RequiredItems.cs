using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace GameBook.Server.Models
{
    public class RequiredItems
    {
        [Key]
        public int RequiredItemsID { get; set; }
        public int LocationID { get; set; }
        public int ItemID { get; set; }

        [ForeignKey(nameof(LocationID))]
        public Location Location { get; set; }

        [ForeignKey(nameof(ItemID))]
        public Item Item { get; set; }
    }
}