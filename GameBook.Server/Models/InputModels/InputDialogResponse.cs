using GameBook.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class InputDialogResponse
    {
        [Required]
        public int DialogID { get; set; }
        public int? NextDialogID { get; set; }
        public required string ResponseText { get; set; }
        public int RelationshipEffect { get; set; }
    }
}