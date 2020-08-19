using RestaurantsList.Contracts;
using RestaurantsList.Models;
using RestaurantsList.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantsList.Abstractions
{
    public interface IRestaurantService
    {
        Task<Restaurant> CreateRestaurantAsync(long cityId, CreateRestaurantRq createRestaurant);

        Task<(IEnumerable<Restaurant> data, int count)> GetRestaurantsByCityAsync(long cityId, GetRestaurantsByCityRq getRestaurantsByCity);
    }
}
