using AutoMapper;
using RestaurantsList.Contracts;
using RestaurantsList.Models;

namespace RestaurantsList.Mapper
{
    public class DomainMapperProfile : Profile
    {
        public DomainMapperProfile()
        {
            CreateMap<CreateCityRq, City>();

            CreateMap<CreateRestaurantRq, Restaurant>();
        }
    }
}
