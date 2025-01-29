using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class ViewInteractOption
    {
        public int OptionID { get; set; }
        public string OptionText { get; set; }
        public ICollection<ViewInteractiblesOption>? Interactibles { get; set; } = null!;
    }
}
