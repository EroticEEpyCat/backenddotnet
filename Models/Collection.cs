namespace webdotnetapp.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Flashcard> Flashcards { get; set; }
    }
}
