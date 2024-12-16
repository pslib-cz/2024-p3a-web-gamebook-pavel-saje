using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string BackgroundImage { get; set; }
        public int RadiationGain { get; set; }
    }
}
