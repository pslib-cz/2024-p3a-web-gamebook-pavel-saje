using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class InteractiblesOption
    {
        [Key]
        public int InteractibleOptionID { get; set; }
        public int InteractibleID { get; set; }
        public int OptionID { get; set; }

        [ForeignKey(nameof(InteractibleID))]
        public Interactible Interactible { get; set; }

        [ForeignKey(nameof(OptionID))]
        public InteractOption Option { get; set; }
    }
}
