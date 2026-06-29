using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.DataSeeding;
using E_Commerce.Infrastructure.Repositries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.Context.StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            //  services.AddScoped<IDataSeeder, DataSeeder>();  not this best  way  becasuse there is ways to seedign so  i will adding  key 
            services.AddKeyedScoped<IDataSeeder, DataSeeder>("Catalog");
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
