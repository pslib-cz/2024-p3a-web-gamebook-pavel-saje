using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataDialog
    {
        [Key]
        public int DialogID { get; set; }
        public int? FromInteractibleID { get; set; }
        public int IteractibleID { get; set; }
        public int? NextDialogID { get; set; }
        public required string Text { get; set; }
        [ForeignKey(nameof(IteractibleID))]
        public DataInteractible? Interactible { get; set; } = null!;
        [ForeignKey(nameof(NextDialogID))]
        public DataDialog? NextDialog { get; set; }
        public ICollection<DataDialogResponse> DialogResponses { get; set; } = new List<DataDialogResponse>();
        [ForeignKey(nameof(FromInteractibleID))]
        public DataInteractible? FromInteractible { get; set; }
    }
}