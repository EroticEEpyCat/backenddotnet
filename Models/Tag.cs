namespace webdotnetapp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FlashcardTag> FlashcardTags { get; set; }
    }
}
