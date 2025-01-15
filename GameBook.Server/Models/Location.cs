using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string? BackgroundImagePath { get; set; }
        
        [NotMapped]
        public IFormFile? BackgroundImage { get; set; }
        public int RadiationGain { get; set; }
        public int? EndID { get; set; }

        [ForeignKey(nameof(EndID))]
        public End? End { get; set; }
    }
}
