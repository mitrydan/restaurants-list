using RestaurantsList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantsList.Abstractions
{
    public interface IRestaurantRepository : IAsyncRepository<Restaurant>
    {
        Task<(IEnumerable<Restaurant>, int count)> GetRestaurantsByCityAsync(long cityId, int pageNumber, int pageSize);

        Task<CityRestaurantJunction> AddCityRestaurantJunction(CityRestaurantJunction cityRestaurantJunction);
    }
}
