using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Models.Products;
using E_Commerce.Domain.Models.OrderModule;
using E_Commerce.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.DataSeeding
{
    public class DataSeeder(StoreDbContext dbContext, ILogger<DataSeeder> logger) : IDataSeeder
    {
        public async Task DataSeedingAsync(CancellationToken ct = default)
        {
            //  Implement the data seeding logic here
            // 1-  Check  there are any pending migrations

            var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);

            if (PendingMigrations.Any())
            {
                logger.LogInformation("Applying pending migrations...");
                await dbContext.Database.MigrateAsync(ct);
                logger.LogInformation("Migrations applied successfully.");
            }
            else
            {
                logger.LogInformation("No pending migrations found.");
            }
            //  craete file path 
            var rootPath = AppContext.BaseDirectory;
            var path = Path.Combine(rootPath, "DataSeed");

            // Order matters: products reference brands and types.
            await SeedIfEmpty<ProductBrand, int>(path, "brands.json", ct);
            await SeedIfEmpty<ProductType, int>(path, "types.json", ct);
            await SeedIfEmpty<Product, int>(path, "products.json", ct);
            await SeedIfEmpty<DeliveryMethod, int>(path, "delivery.json", ct);
        }
        private async Task SeedIfEmpty<T,  TKey> (string rootpath,  string namefile,CancellationToken ct) where T : BaseEntity<TKey>
        {
            //  1- check  dataseeding or not  
            if (await dbContext.Set<T>().AnyAsync(ct)) { 
                logger.LogInformation("There is data in database");
                return; 
            
            }
            //  combine path and check . 
            var filepath = Path.Combine(rootpath, namefile);
            if (!File.Exists(filepath))
            {
                logger.LogInformation($"File {namefile} not Found");
                return;  

            }
            using var openstream = File.OpenRead(filepath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true 
            }; 
           

           var  items  = await JsonSerializer.DeserializeAsync<List<T>>(openstream ,  options , ct);

            if(items?.Count > 0 )
            {
                logger.LogInformation($"Data Seeding ....... {namefile}");
                dbContext.Set<T>().AddRange(items);
                // Save per file so the next file can reference these rows.
                var rows = await dbContext.SaveChangesAsync(ct);
                logger.LogInformation($"Seeded {rows} rows from {namefile}");
            }
        }
    }
}
