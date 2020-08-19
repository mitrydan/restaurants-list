using AutoMapper;
using RestaurantsList.Abstractions;
using RestaurantsList.Contracts;
using RestaurantsList.Exceptions;
using RestaurantsList.Models;
using RestaurantsList.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantsList.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<City> _cityRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(
            IMapper mapper,
            IAsyncRepository<City> cityRepository,
            IRestaurantRepository restaurantRepository
        )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(IAsyncRepository<City>));
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(IRestaurantRepository));
        }

        public async Task<Restaurant> CreateRestaurantAsync(long cityId, CreateRestaurantRq createRestaurant)
        {
            var city = await _cityRepository.GetByIdAsync(cityId);
            if (city == null)
                throw new ApplicationNotFoundException($"Could not found {nameof(City)} with id '{cityId}'");

            var restaurant = _mapper.Map<Restaurant>(createRestaurant);
            var newRestaurant = await _restaurantRepository.AddAsync(restaurant);

            var junction = new CityRestaurantJunction
            {
                CityId = city.Id.Value,
                RestaurantId = newRestaurant.Id.Value
            };
            var newRestaurants = await _restaurantRepository.AddCityRestaurantJunction(junction);

            return newRestaurant;
        }

        public Task<(IEnumerable<Restaurant> data, int count)> GetRestaurantsByCityAsync(long cityId, GetRestaurantsByCityRq getRestaurantsByCity)
        {
            getRestaurantsByCity.Validate();

            return _restaurantRepository.GetRestaurantsByCityAsync(
                cityId,
                getRestaurantsByCity.PageNumber,
                getRestaurantsByCity.PageSize);
        }
    }
}
