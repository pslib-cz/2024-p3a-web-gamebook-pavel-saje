using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class InteractiblesItem
    {
        [Key]
        public int InteractibleItemID { get; set; }
        public int InteractibleID { get; set; }
        public int ItemId { get; set; }

        [ForeignKey(nameof(InteractibleID))]
        public Interactible Interactible { get; set; }

        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }
    }
}
