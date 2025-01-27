using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class ViewLocation
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string BackgroundImagePath { get; set; }
        public int RadiationGain { get; set; }
        public ICollection<ViewLocationContent> LocationContents { get; set; } = new List<ViewLocationContent>();
        public ICollection<ViewItem> RequiredItems { get; set; } = new List<ViewItem>();
    }
}
