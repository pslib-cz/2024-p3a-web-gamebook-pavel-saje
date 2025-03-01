using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class InputBuy
    {
            [Required]
            public int InteractibleID { get; set; }
            [Required]
            public int ItemID { get; set; }
        }
    
}
