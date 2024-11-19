
using Application;
using Persistence;
using Core.CrossCuttingConcerns.Exceptions.Extensions;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDistributedMemoryCache();
            //builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //if(app.Environment.IsProduction())   //create metodu bura girmezse altdakini islet
            app.ConfigureCustomExceptionMiddleware(); // 1.Business ucun bunu islet

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
