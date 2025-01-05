using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public required string Name { get; set; }
        public int? TradeValue { get; set; }
        public bool Stackable { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public ItemCategory Category { get; set; }
    }
}
