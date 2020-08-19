using RestaurantsList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantsList.Abstractions
{
    public interface IAsyncRepository<TBaseModel>
        where TBaseModel : BaseModel

    {
        Task<TBaseModel> GetByIdAsync(long id);

        Task<IEnumerable<TBaseModel>> ListAllAsync();

        Task<TBaseModel> AddAsync(TBaseModel entity);

        Task<bool> UpdateAsync(TBaseModel entity);

        Task<bool> DeleteAsync(TBaseModel entity);
    }
}
