using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class ItemCategory
    {
        [Key]
        public int CategoryID { get; set; }
        public required string Name { get; set; }
    }
}
