using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class DataDialogResponse
    {
        [Key]
        public int DialogResponseID { get; set; }
        public int DialogID { get; set; }
        public int? NextDialogID { get; set; }
        public required string ResponseText { get; set; }
        public int RelationshipEffect { get; set; }
        [ForeignKey(nameof(DialogID))]
        public DataDialog Dialog { get; set; }
        [ForeignKey(nameof(NextDialogID))]
        public DataDialog? NextDialog { get; set; }
    }
}
