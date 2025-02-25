using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class ViewLocationContent
    {
        public int LocationContentID { get; set; }
        public int InteractibleID { get; set; }
        public int LocationID { get; set; }
        public DataLocation Location { get; set; }
        public ViewInteractible Interactible { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int size { get; set; }
    }
}
