using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBook.Server.Models
{
    public class Interactibles
    {
        [Key]
        public int InteractibleID { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        }

    public class InteractibleOptions
    {
        [Key]
        public int InteractibleOptionID { get; set; }

        [ForeignKey(nameof(InteractibleID))]
        public int InteractibleID { get; set; }
        public int OtionID { get; set; }
    }

    public class OptionsEnum
    {
        [Key]
        public int OptionID { get; set; }
        public string OptionText { get; set; }
    }

    public class InteractiblesItems
    {
        [Key]
        public int InteractibleItemID { get; set; }

        [ForeignKey(nameof(InteractibleID))]
        public int InteractibleID { get; set; }
        public int ItemId { get; set; }


    }

    public class NpcDialog
    {
        [Key]
        public int DialogID { get; set; }
        public int IteractibleID { get; set; }
        public int DialogOrder { get; set; }
        public string Text { get; set; }
    }

    public class NpcDialogResponses
    {
        [Key]
        public int DialogResponseID { get; set; }

        [ForeignKey(nameof(DialogID))]
        public int DialogID { get; set; }
        public string ResponseText { get; set; }
        public int RelationshipEffect { get; set; }
    }
}
