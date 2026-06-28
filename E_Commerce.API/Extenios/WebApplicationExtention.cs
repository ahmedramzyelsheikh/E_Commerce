
using E_Commerce.Domain.Contracts;

namespace E_Commerce.API.Extenios
{
    public static class WebApplicationExtention
    {
        public  static  async Task<WebApplication>  SeedingAndMigrationsAsynce(this WebApplication app)
        {
            using var Scoped = app.Services.CreateScope();
            var seeder = Scoped.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await seeder.DataSeedingAync(); 

            return app;
        }
            
            

    }
}
