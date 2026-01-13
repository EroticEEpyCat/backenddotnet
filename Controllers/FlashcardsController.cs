using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webdotnetapp.Data;
using webdotnetapp.Models;

namespace webdotnetapp.Controllers
{
    [ApiController]
    [Route("api/flashcards")]
    public class FlashcardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FlashcardsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateFlashcard([FromBody] Flashcard flashcard)
        {
            
            if (string.IsNullOrWhiteSpace(flashcard.Name) ||
                string.IsNullOrWhiteSpace(flashcard.Description) ||
                flashcard.CollectionId == 0)
            {
                return BadRequest("Please provide name, description, and collectionId.");
            }

            
            var collection = await _context.Collections
                .FirstOrDefaultAsync(c => c.Id == flashcard.CollectionId);

            if (collection == null)
            {
                return BadRequest("Collection not found.");
            }

            
            flashcard.Collection = collection;

            
            _context.Flashcards.Add(flashcard);
            await _context.SaveChangesAsync();

            
            return Ok(flashcard);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllFlashcards()
        {
            var flashcards = await _context.Flashcards.ToListAsync();
            return Ok(flashcards);
        }
    }
}
