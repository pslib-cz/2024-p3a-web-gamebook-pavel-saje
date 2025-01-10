using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class Dialog
    {
        [Key]
        public int DialogID { get; set; }
        public int IteractibleID { get; set; }
        public int DialogOrder { get; set; }
        public required string Text { get; set; }

        [ForeignKey(nameof(IteractibleID))]
        public Interactible Interactible { get; set; } = null!;

        public ICollection<DialogResponse>? DialogResponses { get; set; } = null!;
    }
}
