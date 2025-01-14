using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class End
    {
        [Key]
        public int EndID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
