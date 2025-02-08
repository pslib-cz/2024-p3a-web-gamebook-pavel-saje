namespace GameBook.Server.Models
{
    public class ViewInteractiblesNpc
    {
        public int InteractiblesNpcID { get; set; }
        public int InteractibleID { get; set; }
        public ViewInteractible? Interactible { get; set; }
        public int NpcID { get; set; }
        public ViewNpc? Npc { get; set; }
    }
}
