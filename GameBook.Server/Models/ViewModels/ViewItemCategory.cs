using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class ViewItemCategory
    {
        public int CategoryID { get; set; }
        public required string Name { get; set; }
        public ICollection<ViewItem> Items { get; set; } = new List<ViewItem>();
    }
}
