using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DialogResponse
    {
        [Key]
        public int DialogResponseID { get; set; }
        public int DialogID { get; set; }
        public string ResponseText { get; set; }
        public int RelationshipEffect { get; set; }

        [ForeignKey(nameof(DialogID))]
        public Dialog Dialog { get; set; }
    }
}
