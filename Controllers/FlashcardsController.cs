using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webdotnetapp.Data;
using webdotnetapp.Models;

[ApiController]
[Route("api/[controller]")]
public class FlashcardsController : ControllerBase
{
    private readonly AppDbContext _context;

    public FlashcardsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetFlashcards()
    {
        var flashcards = await _context.Flashcards
            .Include(f => f.Collection)
            .ToListAsync();

        return Ok(flashcards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFlashcard(int id)
    {
        var flashcard = await _context.Flashcards
            .Include(f => f.Collection)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (flashcard == null)
            return NotFound();

        return Ok(flashcard);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFlashcard([FromBody] Flashcard flashcard)
    {
        if (flashcard == null)
            return BadRequest("Flashcard data is required.");

        if (string.IsNullOrWhiteSpace(flashcard.Name))
            return BadRequest("Flashcard Name is required.");

        if (flashcard.CollectionId <= 0)
            return BadRequest("Valid CollectionId is required.");

        var collectionExists = await _context.Collections.AnyAsync(c => c.Id == flashcard.CollectionId);
        if (!collectionExists)
            return BadRequest("Collection does not exist.");

        _context.Flashcards.Add(flashcard);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFlashcard), new { id = flashcard.Id }, flashcard);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFlashcard(int id, [FromBody] Flashcard flashcard)
    {
        if (flashcard == null || id != flashcard.Id)
            return BadRequest("Invalid flashcard data.");

        var existing = await _context.Flashcards.FindAsync(id);
        if (existing == null)
            return NotFound();

        existing.Name = flashcard.Name;
        existing.Description = flashcard.Description;
        existing.CollectionId = flashcard.CollectionId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFlashcard(int id)
    {
        var flashcard = await _context.Flashcards.FindAsync(id);
        if (flashcard == null)
            return NotFound();

        _context.Flashcards.Remove(flashcard);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
