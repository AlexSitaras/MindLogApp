using Microsoft.EntityFrameworkCore;
using MindLog.Models;
using MindLog.Models.Entity;

namespace MindLog.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MoodEntry> MoodEntries { get; set; }
    }
}
