namespace webdotnetapp.Models
{
    public class FlashcardTag
    {
        public int FlashcardId { get; set; }
        public Flashcard Flashcard { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
