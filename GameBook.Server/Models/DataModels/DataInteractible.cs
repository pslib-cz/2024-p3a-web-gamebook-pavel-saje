using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataInteractible
    {
        [Key]
        public int InteractibleID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
