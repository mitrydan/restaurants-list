using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantsList.Abstractions;
using RestaurantsList.DataAccess.MSSQL.Repositories;

namespace RestaurantsList.DataAccess.MSSQL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessMsSql(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));

            serviceCollection.AddTransient(typeof(IAsyncRepository<Models.City>), typeof(CityRepository));
            serviceCollection.AddTransient(typeof(IRestaurantRepository), typeof(RestaurantRepository));
        }
    }
}
