using Microsoft.AspNetCore.Mvc;
using webdotnetapp.Data;
using webdotnetapp.Models;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    public UsersController(AppDbContext context) { _context = context; }

    [HttpGet] public async Task<IActionResult> GetUsers() => Ok(await _context.Users.ToListAsync());
    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }
}
