using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SeedManeger
    {
        public static async Task Seed(IServiceProvider service)
        {
            await SeedRoles(service);
            await SeedAdminUser(service);
        }

        private static async Task SeedRoles(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Role.Admin));
            await roleManager.CreateAsync(new IdentityRole(Role.User));
        }

        private static async Task SeedAdminUser(IServiceProvider service)
        {
            var context = service.GetRequiredService<AppDbContext>();
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            var adminUser = await context.Users.FirstOrDefaultAsync(user => user.UserName == "AuthenticationAdmin");

            if (adminUser is null)
            {
                adminUser = new User { UserName = "AuthenticationAdmin", Email = "dkred63@gmail.com" };
                await userManager.CreateAsync(adminUser, "ASecretPassword1+");
                await userManager.AddToRoleAsync(adminUser, Role.Admin);
            }
        }
    }
}