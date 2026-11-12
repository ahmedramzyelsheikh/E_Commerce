using E_Commerce.Application.Profiles;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application
{
    public static class ApplicationServicesExtensions
    {
        public  static  IServiceCollection AddApplicationServices(this IServiceCollection services  )
        {
            services.AddAutoMapper(c=> { }, typeof(ApplicationServicesExtensions).Assembly) ;
            services.AddScoped<IProductService, ProductService>();  
            services.AddScoped<IBasketService, BasketService>();

            return services; 
        }
    }
}
