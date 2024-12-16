using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class InteractOption
    {
        [Key]
        public int OptionID { get; set; }
        public string OptionText { get; set; }
    }
}
