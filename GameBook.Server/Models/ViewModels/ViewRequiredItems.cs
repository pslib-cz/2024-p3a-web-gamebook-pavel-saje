namespace GameBook.Server.Models
{
    public class ViewRequiredItems
    {
        public int LocationID { get; set; }
        public int ItemID { get; set; }
        public ViewLocation Location { get; set; }
        public ViewItem Item { get; set; }
    }
}
