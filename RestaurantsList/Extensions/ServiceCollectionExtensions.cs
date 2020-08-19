using Microsoft.Extensions.DependencyInjection;
using RestaurantsList.Abstractions;
using RestaurantsList.Services;

namespace RestaurantsList.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(ICityService), typeof(CityService));
            serviceCollection.AddTransient(typeof(IRestaurantService), typeof(RestaurantService));
        }
    }
}
