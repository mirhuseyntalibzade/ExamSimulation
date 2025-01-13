using Microsoft.AspNetCore.Identity;
using TestCORE.Models;

namespace TestMVC
{
    public class DatabaseSeeder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(IServiceProvider serviceProvider,
               UserManager<AppUser> userManager,
               RoleManager<IdentityRole> roleManager)
        {
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();

            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            var adminUser = await _userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                var newAdmin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@example.com"
                };

                var result = await _userManager.CreateAsync(newAdmin, "AdminPassword123!");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }
}
