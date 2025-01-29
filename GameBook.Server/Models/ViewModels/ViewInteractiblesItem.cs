using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class ViewInteractiblesItem
    {
        public int InteractibleID { get; set; }
        public ViewInteractible Interactible { get; set; }
        public int ItemId { get; set; }
        public ViewItem Item { get; set; }
    }
}
