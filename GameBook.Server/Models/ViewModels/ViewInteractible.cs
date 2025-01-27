using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class ViewInteractible
    {
        public int InteractibleID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ICollection<ViewInteractiblesOption>? InteractOptions { get; set; } = new List<ViewInteractiblesOption>();
        public ICollection<ViewLocationContent> LocationContents { get; set; } = new List<ViewLocationContent>();
    }
}
