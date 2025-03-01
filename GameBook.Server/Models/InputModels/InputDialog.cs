using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class InputDialog
    {
        public int? FromInteractibleID { get; set; }
        [Required]
        public int IteractibleID { get; set; }
        public int? NextDialogID { get; set; }
        [Required]
        public required string Text { get; set; }
    }
}
