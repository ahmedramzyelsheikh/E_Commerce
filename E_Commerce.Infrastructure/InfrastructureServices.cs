using E_Commerce.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
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
            services.AddDbContext<Data.Context.StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentity<E_Commerce.Domain.Models.Identity.ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
                .AddEntityFrameworkStores<Data.Context.StoreIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<E_Commerce.Application.Services.Contracts.ITokenService, E_Commerce.Infrastructure.Services.TokenService>();
            services.AddScoped<E_Commerce.Application.Services.Contracts.IAuthenticationService, E_Commerce.Infrastructure.Services.AuthenticationService>();

            //  services.AddScoped<IDataSeeder, DataSeeder>();  not this best  way  becasuse there is ways to seedign so  i will adding  key 
            services.AddKeyedScoped<IDataSeeder, DataSeeder>("Catalog");
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderService, Services.OrderService>();

            services.AddSingleton<StackExchange.Redis.IConnectionMultiplexer>(sp =>
            {
                var connection = configuration.GetConnectionString("Redis");
                return StackExchange.Redis.ConnectionMultiplexer.Connect(connection!);
            });

            return services;
        }
    }
}
