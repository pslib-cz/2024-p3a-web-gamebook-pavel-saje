using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataInteractiblesItem
    {
        [Key]
        public int InteractiblesItemID { get; set; }
        public int InteractibleID { get; set; }
        [ForeignKey(nameof(InteractibleID))]
        public DataInteractible Interactible { get; set; }

        public int ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public DataItem Item { get; set; }
    }
}
