using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantsList.Abstractions;
using RestaurantsList.DataAccess.MSSQL.Entities;
using RestaurantsList.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantsList.DataAccess.MSSQL.Repositories
{
    public abstract class AsyncRepository<TBaseModel, TBaseEntity> : IAsyncRepository<TBaseModel>
        where TBaseModel : BaseModel
        where TBaseEntity : BaseEntity
    {
        protected IMapper Mapper { get; private set; }

        protected ApplicationDbContext DbContext { get; private set; }

        protected AsyncRepository(IMapper mapper, ApplicationDbContext dbContext)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
        }

        public async Task<TBaseModel> AddAsync(TBaseModel entity)
        {
            var dbEntity = Mapper.Map<TBaseEntity>(entity);
            await DbContext.Set<TBaseEntity>().AddAsync(dbEntity);
            var result = await DbContext.SaveChangesAsync() == 1 ? dbEntity : null;
            return Mapper.Map<TBaseModel>(result);
        }

        public async Task<bool> DeleteAsync(TBaseModel entity)
        {
            var dbEntity = Mapper.Map<TBaseEntity>(entity);
            DbContext.Attach(dbEntity).State = EntityState.Deleted;
            return await DbContext.SaveChangesAsync() == 1;
        }

        public async Task<TBaseModel> GetByIdAsync(long id)
        {
            var result = await DbContext.Set<TBaseEntity>().FindAsync(id);
            return Mapper.Map<TBaseModel>(result);
        }

        public async Task<IEnumerable<TBaseModel>> ListAllAsync()
        {
            var result = await DbContext.Set<TBaseEntity>().ToListAsync();
            return Mapper.Map<IEnumerable<TBaseModel>>(result);
        }

        public async Task<bool> UpdateAsync(TBaseModel entity)
        {
            var dbEntity = Mapper.Map<TBaseEntity>(entity);
            DbContext.Attach(dbEntity).State = EntityState.Modified;
            return await DbContext.SaveChangesAsync() == 1;
        }
    }
}
