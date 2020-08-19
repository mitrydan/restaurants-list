using AutoMapper;
using RestaurantsList.Abstractions;

namespace RestaurantsList.DataAccess.MSSQL.Repositories
{
    public class CityRepository : AsyncRepository<Models.City, Entities.City>, IAsyncRepository<Models.City>
    {
        public CityRepository(IMapper mapper, ApplicationDbContext dbContext)
            : base(mapper, dbContext)
        { }
    }
}
