using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class Interactible
    {
        [Key]
        public int InteractibleID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
