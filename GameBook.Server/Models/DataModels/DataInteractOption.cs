using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataInteractOption
    {
        [Key]
        public int OptionID { get; set; }
        public string OptionText { get; set; }
        public ICollection<DataInteractiblesOption>? Interactibles { get; set; } = null!;
    }
}
