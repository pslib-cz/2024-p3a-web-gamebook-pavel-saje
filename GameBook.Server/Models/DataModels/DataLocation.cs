using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataLocation
    {
        [Key]
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string BackgroundImagePath { get; set; }
        public int RadiationGain { get; set; }
        public ICollection<DataLocationContent> LocationContents { get; set; } = new List<DataLocationContent>();
        public ICollection<DataRequiredItems> RequiredItems { get; set; } = new List<DataRequiredItems>();
    }
}
