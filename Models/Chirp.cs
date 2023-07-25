using Microsoft.EntityFrameworkCore;

namespace Chirp.Models
{
    public class ChirpData
    {
        public int Id { get; set; }
        public string BodyText { get; set; } = "empty";
        public DateTime PublishedTime { get; set; } = DateTime.MinValue;
    }

    class ChirpDb : DbContext
    {
        public ChirpDb(DbContextOptions options) : base(options) { }
        public DbSet<ChirpData> Chirps { get; set; } = null!;
    }
}