using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class ViewDialog
    {
        public int DialogID { get; set; }
        public int? FromInteractibleID { get; set; }
        public int IteractibleID { get; set; }
        public int? NextDialogID { get; set; }
        public required string Text { get; set; }
        public ViewInteractible Interactible { get; set; } = null!;
        public ViewDialog? NextDialog { get; set; }
        public ICollection<ViewDialogResponse>? DialogResponses { get; set; } = null!;
        public ViewInteractible? FromInteractible { get; set; }
    }
}
