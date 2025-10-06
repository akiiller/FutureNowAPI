using Microsoft.EntityFrameworkCore;
using FutureNowAPI.Models;

namespace FutureNowAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Music> Music { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Podcast> Podcasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Music>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Artist).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Genre).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Podcast>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Host).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
        });

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Music>().HasData(
            new Music
            {
                Id = 1,
                Title = "Summer Vibes",
                Artist = "The Sunset Band",
                Album = "Coastal Dreams",
                DurationSeconds = 245,
                Genre = "Pop",
                Url = "https://example.com/music/summer-vibes.mp3",
                CreatedAt = DateTime.UtcNow
            },
            new Music
            {
                Id = 2,
                Title = "Midnight Jazz",
                Artist = "Blue Note Trio",
                Album = "Late Night Sessions",
                DurationSeconds = 320,
                Genre = "Jazz",
                Url = "https://example.com/music/midnight-jazz.mp3",
                CreatedAt = DateTime.UtcNow
            },
            new Music
            {
                Id = 3,
                Title = "Electric Dreams",
                Artist = "Synthwave Masters",
                Album = "Neon Nights",
                DurationSeconds = 280,
                Genre = "Electronic",
                Url = "https://example.com/music/electric-dreams.mp3",
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Video>().HasData(
            new Video
            {
                Id = 1,
                Title = "Introduction to Quantum Computing",
                Description = "A comprehensive guide to understanding quantum computing basics",
                DurationSeconds = 1800,
                Category = "Education",
                Url = "https://example.com/videos/quantum-computing-intro.mp4",
                CreatedAt = DateTime.UtcNow
            },
            new Video
            {
                Id = 2,
                Title = "Cooking Perfect Pasta",
                Description = "Master the art of cooking authentic Italian pasta",
                DurationSeconds = 900,
                Category = "Cooking",
                Url = "https://example.com/videos/perfect-pasta.mp4",
                CreatedAt = DateTime.UtcNow
            },
            new Video
            {
                Id = 3,
                Title = "Yoga for Beginners",
                Description = "Start your yoga journey with these simple poses",
                DurationSeconds = 1200,
                Category = "Fitness",
                Url = "https://example.com/videos/yoga-beginners.mp4",
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Podcast>().HasData(
            new Podcast
            {
                Id = 1,
                Title = "The Future of AI",
                Episode = "Episode 42",
                Host = "Dr. Sarah Mitchell",
                DurationSeconds = 2700,
                Category = "Technology",
                Url = "https://example.com/podcasts/future-of-ai-ep42.mp3",
                CreatedAt = DateTime.UtcNow
            },
            new Podcast
            {
                Id = 2,
                Title = "True Crime Stories",
                Episode = "Episode 15",
                Host = "Detective John Rivers",
                DurationSeconds = 3600,
                Category = "Crime",
                Url = "https://example.com/podcasts/true-crime-ep15.mp3",
                CreatedAt = DateTime.UtcNow
            },
            new Podcast
            {
                Id = 3,
                Title = "Business Insights",
                Episode = "Episode 88",
                Host = "Emma Thompson",
                DurationSeconds = 1800,
                Category = "Business",
                Url = "https://example.com/podcasts/business-insights-ep88.mp3",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
