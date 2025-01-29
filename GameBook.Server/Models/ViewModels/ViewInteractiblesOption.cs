using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class ViewInteractiblesOption
    {
        public int InteractibleID { get; set; }
        public ViewInteractible Interactible { get; set; } = null!;
        public int OptionID { get; set; }
        public ViewInteractOption Option { get; set; } = null!;
    }
}