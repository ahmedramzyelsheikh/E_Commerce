using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Models.Identity;
using E_Commerce.Infrastructure.Data.Context;
using E_Commerce.Infrastructure.DataSeeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedAndMigrateAsync(this WebApplication app)
        {
            using var Scoped = app.Services.CreateScope();
            var services = Scoped.ServiceProvider;

            // Store database: migrations + catalog and delivery methods
            var seeder = services.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await seeder.DataSeedingAsync();

            // Identity database: migrations + roles and the default admin user
            var identityContext = services.GetRequiredService<StoreIdentityDbContext>();
            if ((await identityContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await identityContext.Database.MigrateAsync();
            }

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await IdentityDataSeeder.SeedUsersAndRolesAsync(userManager, roleManager);

            return app;
        }
    }
}
