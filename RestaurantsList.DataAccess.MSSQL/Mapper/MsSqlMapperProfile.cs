using AutoMapper;

namespace RestaurantsList.DataAccess.MSSQL.Mapper
{
    public class MsSqlMapperProfile : Profile
    {
        public MsSqlMapperProfile()
        {
            CreateMap<Models.City, Entities.City>();

            CreateMap<Models.Restaurant, Entities.Restaurant>();

            CreateMap<Models.CityRestaurantJunction, Entities.CityRestaurantJunction>();


            CreateMap<Entities.City, Models.City>();

            CreateMap<Entities.Restaurant, Models.Restaurant>();

            CreateMap<Entities.CityRestaurantJunction, Models.CityRestaurantJunction>();
        }
    }
}
