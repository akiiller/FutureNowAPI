using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutureNowAPI.Data;
using FutureNowAPI.Models;

namespace FutureNowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MusicController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MusicController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Music>>> GetAll()
    {
        return await _context.Music.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Music>> GetById(int id)
    {
        var music = await _context.Music.FindAsync(id);

        if (music == null)
        {
            return NotFound();
        }

        return music;
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Music>>> Search([FromQuery] string? genre, [FromQuery] string? artist, [FromQuery] string? title)
    {
        var query = _context.Music.AsQueryable();

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(m => m.Genre.Contains(genre));
        }

        if (!string.IsNullOrEmpty(artist))
        {
            query = query.Where(m => m.Artist.Contains(artist));
        }

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(m => m.Title.Contains(title));
        }

        return await query.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Music>> Create(Music music)
    {
        _context.Music.Add(music);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = music.Id }, music);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Music music)
    {
        if (id != music.Id)
        {
            return BadRequest();
        }

        _context.Entry(music).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await MusicExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var music = await _context.Music.FindAsync(id);
        if (music == null)
        {
            return NotFound();
        }

        _context.Music.Remove(music);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> MusicExists(int id)
    {
        return await _context.Music.AnyAsync(e => e.Id == id);
    }
}
