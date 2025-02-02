using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataEnd
    {
        [Key]
        public int EndID { get; set; }
        public required string Text { get; set; }
        public string? ImagePath { get; set; }
    }
}