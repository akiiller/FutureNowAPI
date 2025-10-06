namespace FutureNowAPI.Models;

public class Podcast
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Episode { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int DurationSeconds { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
