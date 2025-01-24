using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataItemCategory
    {
        [Key]
        public int CategoryID { get; set; }
        public required string Name { get; set; }

        public ICollection<DataItem> Items { get; set; } = new List<DataItem>();
    }
}
