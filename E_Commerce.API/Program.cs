using E_Commerce.API.Extensions;
using E_Commerce.Application;
using E_Commerce.Application.Profilers;
using E_Commerce.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;
namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddInfrastructureServices(builder.Configuration); //  this  line  DI  comming  from  Infrastructure layer . 
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddServicesApplication();   //  this line   amke DI  comming  from  Application layer .  
            builder.Services.Configure<UrlSetteings>(builder.Configuration.GetSection("UrlSettings")); //  
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            await app.SeedingAndMigrationsAsynce(); 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(new StaticFileOptions  //  this  middleware make explicit  photo comming  from  files    not wwwroot .  
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")) , 
                RequestPath = "/Files"
                
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
