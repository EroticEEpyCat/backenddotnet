using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webdotnetapp.Data;
using webdotnetapp.Models;

namespace webdotnetapp.Controllers
{
    [ApiController]
    [Route("api/collections")]
    public class CollectionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CollectionsController(AppDbContext context) => _context = context;

        // GET all collections
        [HttpGet]
        public async Task<IActionResult> GetCollections() =>
            Ok(await _context.Collections.ToListAsync());

        // POST create collection
        [HttpPost]
        public async Task<IActionResult> CreateCollection([FromBody] Collection collection)
        {
            if (string.IsNullOrWhiteSpace(collection.Name))
                return BadRequest("Name is required");

            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();
            return Ok(collection);
        }

        // PUT update collection
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollection(int id, [FromBody] Collection updated)
        {
            var col = await _context.Collections.FindAsync(id);
            if (col == null) return NotFound();

            if (string.IsNullOrWhiteSpace(updated.Name))
                return BadRequest("Name is required");

            col.Name = updated.Name;
            await _context.SaveChangesAsync();
            return Ok(col);
        }

        // DELETE collection
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            var col = await _context.Collections.FindAsync(id);
            if (col == null) return NotFound();

            _context.Collections.Remove(col);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
