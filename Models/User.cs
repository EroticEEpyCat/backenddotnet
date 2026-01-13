using System.Collections.Generic;

namespace webdotnetapp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // store plain text for now (hash later!)
        public List<Collection> Collections { get; set; }
    }
}
