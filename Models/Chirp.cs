using Microsoft.EntityFrameworkCore;

namespace Chirp.Models
{
    public class ChirpData
    {
        public int Uuid { get; set; }
        public string BodyText { get; set; } = "empty";
        public string PublishedTime { get; set; } = "no time";
    }

    class ChirpDb : DbContext
    {
        public ChirpDb(DbContextOptions options) : base(options) { }
        public DbSet<ChirpData> Chirps { get; set; } = null!;
    }
}