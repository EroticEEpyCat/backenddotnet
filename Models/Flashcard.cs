using System.Text.Json.Serialization;

namespace webdotnetapp.Models
{
    public class Flashcard
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int CollectionId { get; set; }

        [JsonIgnore]
        public Collection? Collection { get; set; }

        [JsonIgnore]
        public List<FlashcardTag>? FlashcardTags { get; set; } = new List<FlashcardTag>();
    }
}
