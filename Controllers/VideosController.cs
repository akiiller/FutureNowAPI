using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutureNowAPI.Data;
using FutureNowAPI.Models;

namespace FutureNowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VideosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Video>>> GetAll()
    {
        return await _context.Videos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Video>> GetById(int id)
    {
        var video = await _context.Videos.FindAsync(id);

        if (video == null)
        {
            return NotFound();
        }

        return video;
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Video>>> Search([FromQuery] string? category, [FromQuery] string? title)
    {
        var query = _context.Videos.AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(v => v.Category.Contains(category));
        }

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(v => v.Title.Contains(title));
        }

        return await query.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Video>> Create(Video video)
    {
        _context.Videos.Add(video);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = video.Id }, video);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Video video)
    {
        if (id != video.Id)
        {
            return BadRequest();
        }

        _context.Entry(video).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await VideoExists(id))
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
        var video = await _context.Videos.FindAsync(id);
        if (video == null)
        {
            return NotFound();
        }

        _context.Videos.Remove(video);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> VideoExists(int id)
    {
        return await _context.Videos.AnyAsync(e => e.Id == id);
    }
}
