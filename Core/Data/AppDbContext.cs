using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<ProblemTag> ProblemTags { get; set; }
        public DbSet<Solved> Solved { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
