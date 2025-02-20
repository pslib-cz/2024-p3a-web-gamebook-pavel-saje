using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class ViewLocationContent
    {
        public int InteractibleID { get; set; }
        public ViewInteractible Interactible { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int size { get; set; }
    }
}
