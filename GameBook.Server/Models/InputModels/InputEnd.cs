using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class InputEnd
    {
        [Required]
        public int LocationID { get; set; }
        [Required]
        public int DialogID { get; set; }
    }
}
