using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataItem
    {
        [Key]
        public int ItemID { get; set; }
        public required string Name { get; set; }
        public int? TradeValue { get; set; }
        public bool Stackable { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public DataItemCategory Category { get; set; }
        public ICollection<DataRequiredItems> RequiredInLocations { get; set; } = new List<DataRequiredItems>();
    }
}
