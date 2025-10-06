using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutureNowAPI.Data;
using FutureNowAPI.Models;

namespace FutureNowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PodcastsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PodcastsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Podcast>>> GetAll()
    {
        return await _context.Podcasts.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Podcast>> GetById(int id)
    {
        var podcast = await _context.Podcasts.FindAsync(id);

        if (podcast == null)
        {
            return NotFound();
        }

        return podcast;
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Podcast>>> Search([FromQuery] string? category, [FromQuery] string? host, [FromQuery] string? title)
    {
        var query = _context.Podcasts.AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category.Contains(category));
        }

        if (!string.IsNullOrEmpty(host))
        {
            query = query.Where(p => p.Host.Contains(host));
        }

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(p => p.Title.Contains(title));
        }

        return await query.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Podcast>> Create(Podcast podcast)
    {
        _context.Podcasts.Add(podcast);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = podcast.Id }, podcast);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Podcast podcast)
    {
        if (id != podcast.Id)
        {
            return BadRequest();
        }

        _context.Entry(podcast).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await PodcastExists(id))
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
        var podcast = await _context.Podcasts.FindAsync(id);
        if (podcast == null)
        {
            return NotFound();
        }

        _context.Podcasts.Remove(podcast);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> PodcastExists(int id)
    {
        return await _context.Podcasts.AnyAsync(e => e.Id == id);
    }
}
