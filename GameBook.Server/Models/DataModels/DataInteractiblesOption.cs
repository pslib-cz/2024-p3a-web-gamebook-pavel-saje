using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataInteractiblesOption
    {
        [Key]
        public int InteractiblesOptionID { get; set; }
        public int InteractibleID { get; set; }
        [ForeignKey(nameof(InteractibleID))]
        public DataInteractible Interactible { get; set; } = null!;

        public int OptionID { get; set; }
        [ForeignKey(nameof(OptionID))]
        public DataInteractOption Option { get; set; } = null!;
    }
}