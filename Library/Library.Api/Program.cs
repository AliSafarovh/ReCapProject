
using LibraryApi.Data.Entities;
using LibraryApi.Data.Mapper;
using LibraryApi.Services.Implements;
using LibraryApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });
            builder.Services.AddScoped<IBookRepository,BookRepository>();
            builder.Services.AddScoped<IAuthorRepository,AuthorRepository>();
            builder.Services.AddScoped<IGenreRepository,GenreRepository>();
            builder.Services.AddScoped<IFileRepository, FileRepository>();
            builder.Services.AddAutoMapper(typeof(MapProfile));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
