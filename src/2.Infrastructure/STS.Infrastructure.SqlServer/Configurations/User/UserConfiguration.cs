using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STS.Domain.Core.Entities.User;

namespace STS.Infrastructure.SqlServer.Configurations.User
{
    public class UserConfiguration
    {

        public static void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser()
            {
                Id = 1,
                UserName = "Admin@gmail.com",
                FirstName = "Admin",
                LastName = "Admini",
                PhoneNumber = "09123456789",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "Admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                LockoutEnabled = false,
                SecurityStamp = "test",
                ConcurrencyStamp = "test",
                CityId = 1,
                Balance = 0,
                RegisteredAt = new DateTime(2024,08,24),
                PasswordHash = "AQAAAAIAAYagAAAAEH5idFao3sv5eJMqp6lokCDHTgl6DQeHXXxWOz1dFMZrbbFjYmwJgxZ3k7V6Y5BLRg=="
            };
            builder.Entity<ApplicationUser>().HasData(user);

            // Seed Roles
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int>() { Id = 2, Name = "Client", NormalizedName = "CLIENT" },
                new IdentityRole<int>() { Id = 3, Name = "Expert", NormalizedName = "EXPERT" }
            );

            //Seed Role To Users
            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>() { UserId = 1, RoleId = 1 }
            );
        }
    }
}
