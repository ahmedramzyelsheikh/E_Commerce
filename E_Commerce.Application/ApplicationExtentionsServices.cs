using E_Commerce.Application.Profilers;
using E_Commerce.Application.Services.Contracts;
using E_Commerce.Application.Services.Implemetion;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application
{
    public static class ApplicationExtentionsServices
    {
        public  static  IServiceCollection AddServicesApplication(this IServiceCollection services  )
        {
            services.AddAutoMapper(c=> { }, typeof(ApplicationExtentionsServices).Assembly) ;
            services.AddScoped<IProductService, ProductServices>();  
            services.AddScoped<IBasketService, BasketService>();

            return services; 
        }
    }
}
