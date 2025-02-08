using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class DataInteractiblesNpc
    {
        [Key]
        public int InteractiblesNpcID { get; set; }
        public int InteractibleID { get; set; }
        [ForeignKey(nameof(InteractibleID))]
        public DataInteractible? Interactible { get; set; }
        public int NpcID { get; set; }
        [ForeignKey(nameof(NpcID))]
        public DataNpc? Npc { get; set; }
    }
}
