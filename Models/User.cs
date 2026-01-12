namespace webdotnetapp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Collection> Collections { get; set; }
    }
}
