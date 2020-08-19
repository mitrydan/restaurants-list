using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantsList.Abstractions;
using RestaurantsList.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsList.DataAccess.MSSQL.Repositories
{
    public class RestaurantRepository : AsyncRepository<Models.Restaurant, Entities.Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(IMapper mapper, ApplicationDbContext dbContext)
            : base(mapper, dbContext)
        { }

        public async Task<Models.CityRestaurantJunction> AddCityRestaurantJunction(CityRestaurantJunction cityRestaurantJunction)
        {
            var dbEntity = Mapper.Map<Entities.CityRestaurantJunction>(cityRestaurantJunction);
            await DbContext.CityRestaurantJunction.AddAsync(dbEntity);
            var result = await DbContext.SaveChangesAsync() == 1 ? dbEntity : null;
            return Mapper.Map<Models.CityRestaurantJunction>(result);
        }

        public async Task<(IEnumerable<Models.Restaurant>, int count)> GetRestaurantsByCityAsync(long cityId, int pageNumber, int pageSize)
        {
            var count = await DbContext.Set<Entities.CityRestaurantJunction>()
                .Where(x => x.CityId == cityId)
                .CountAsync();

            var result = await DbContext.Set<Entities.CityRestaurantJunction>()
                .Where(x => x.CityId == cityId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(x => x.Restaurant)
                .ToListAsync();

            return (
                result.Select(x => Mapper.Map<Models.Restaurant>(x.Restaurant)),
                count
            );
        }
    }
}
