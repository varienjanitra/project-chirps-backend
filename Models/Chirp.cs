using Microsoft.EntityFrameworkCore;

// Data model of the chirp data
namespace Chirp.Models
{
    public class ChirpData
    {
        public int Id { get; set; }
        public string BodyText { get; set; } = "empty";
        public DateTime PublishedTime { get; set; } = DateTime.MinValue;
    }
}

// Database context of a chirp database
namespace Chirp.Database
{
    using Chirp.Models;
    class ChirpDb : DbContext
    {
        public ChirpDb(DbContextOptions<ChirpDb> options) : base(options) { }
        public DbSet<ChirpData> Chirps { get; set; } = null!;
    }
}