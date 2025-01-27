using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameBook.Server.Models
{
    public class ViewDialogResponse
    {
        public int DialogResponseID { get; set; }
        public int DialogID { get; set; }
        public int? NextDialogID { get; set; }
        public required string ResponseText { get; set; }
        public int RelationshipEffect { get; set; }
        public ViewDialog Dialog { get; set; }
        public ViewDialog? NextDialog { get; set; }
    }
}
