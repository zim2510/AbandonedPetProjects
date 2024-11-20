using Microsoft.EntityFrameworkCore;
using TalkOTC.Data.Entities;

namespace TalkOTC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<UserRoom> UserRooms { get; set; }
    }
}
