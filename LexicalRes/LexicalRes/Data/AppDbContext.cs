using LexicalRes.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LexicalRes.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<WordList> WordLists { get; set; }
        public virtual DbSet<WordWordList> WordWordLists { get; set; }
        public virtual DbSet<Meaning> Meanings { get; set; }
        public virtual DbSet<Example> Examples { get; set; }
        public virtual DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WordConfig());
            modelBuilder.ApplyConfiguration(new WordListConfig());
            modelBuilder.ApplyConfiguration(new MeaningConfig());
            modelBuilder.ApplyConfiguration(new ExampleConfig());
        }
    }
}
