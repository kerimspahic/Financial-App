using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class SeedData
    {
        private const string Admin = "Admin";
        private const string User = "User";

        public static async Task InitializeAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            // Check if the admin user already exists
            var adminUser = await context.Users.FirstOrDefaultAsync(user => user.UserName == "admin");

            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = "admin",
                    Email = "dkred63@gmail.com",
                    FirstName = "Admin",
                    LastName = "User"
                };
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
            }
        }
    }
}
