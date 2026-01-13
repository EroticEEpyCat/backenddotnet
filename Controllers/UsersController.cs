using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webdotnetapp.Data;
using webdotnetapp.Models;

namespace webdotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Email and password are required.");

            var existing = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existing != null)
                return BadRequest("Email already registered.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("Email and password are required.");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email
            });
        }
    }

    // Model for login request
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
