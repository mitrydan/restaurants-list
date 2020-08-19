using AutoMapper;
using RestaurantsList.Abstractions;
using RestaurantsList.Contracts;
using RestaurantsList.Models;
using System;
using System.Threading.Tasks;

namespace RestaurantsList.Services
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<City> _cityRepository;

        public CityService(IMapper mapper, IAsyncRepository<City> cityRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(IAsyncRepository<City>));
        }

        public Task<City> CreateCityAsync(CreateCityRq createCity)
        {
            var city = _mapper.Map<City>(createCity);
            return _cityRepository.AddAsync(city);
        }
    }
}
