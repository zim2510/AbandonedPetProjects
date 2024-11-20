using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace Core.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = "zim2510",
                    Name = "Muhaiminul Islam Jim",
                    Password = "912920",
                    UserType = UserType.Admin
                }
            );
        }
    }
}
