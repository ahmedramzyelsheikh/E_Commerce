using E_Commerce.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Infrastructure.DataSeeding
{
    public class IdentityDataSeeder
    {
        public static async Task SeedUsersAndRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("SuperAdmin"))
            {
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    DisplayName = "Mohamed",
                    Email = "mohamed@ecommerce.com",
                    UserName = "mohamed@ecommerce.com",
                    PhoneNumber = "1234567890",
                    Address = new Address
                    {
                        FirstName = "Mohamed",
                        LastName = "Admin",
                        Street = "10 Main St",
                        City = "Cairo"
                    }
                };

                var result = await userManager.CreateAsync(user, "Pa$$w0rd");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "SuperAdmin");
                }
            }
        }
    }
}
