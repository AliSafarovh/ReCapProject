using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Contexts
{
    public class BaseDbContext:DbContext
    {
        public IConfiguration Configuration{ get; set; }
        public DbSet<Brand>Brands { get; set; }
        public DbSet<Model>Models { get; set; }
        public DbSet<Fuel>Fuels { get; set; }
        public DbSet<Transmission>Transmissions { get; set; }
        public DbSet<Car>Cars { get; set; }
        public BaseDbContext(DbContextOptions dbContextOptions,IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
            Database.EnsureCreated(); //db yaradildiqindan emin ol.
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
