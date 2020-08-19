using Microsoft.EntityFrameworkCore;
using RestaurantsList.DataAccess.MSSQL.Entities;
using System.Reflection;

namespace RestaurantsList.DataAccess.MSSQL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<City> City { get; set; }

        public DbSet<Restaurant> Restaurant { get; set; }

        public DbSet<CityRestaurantJunction> CityRestaurantJunction { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
