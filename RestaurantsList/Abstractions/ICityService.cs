using RestaurantsList.Contracts;
using RestaurantsList.Models;
using System.Threading.Tasks;

namespace RestaurantsList.Abstractions
{
    public interface ICityService
    {
        Task<City> CreateCityAsync(CreateCityRq createCity);
    }
}
