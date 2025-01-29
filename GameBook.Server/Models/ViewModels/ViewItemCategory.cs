using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class ViewItemCategory
    {
        public int CategoryID { get; set; }
        public required string Name { get; set; }
    }
}
