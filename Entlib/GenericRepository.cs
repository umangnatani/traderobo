using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Tasks;

namespace Entlib
{
    public interface IEntity
    {
        int Id { get; set; }
    }


    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(int id);

        Task Maintain(TEntity entity, bool SaveChanges);

        Task Create(TEntity entity, bool SaveChanges);

        Task Update(TEntity entity, bool SaveChanges);

        Task Delete(int id, bool SaveChanges);
    }


    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntity
    {
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbContext.Set<TEntity>()
                        .AsNoTracking()
                        .FirstAsync(e => e.Id == id);
        }


        public async Task Maintain(TEntity entity, bool SaveChanges = true)
        {
            if (entity.Id > 0)
                await Update(entity, SaveChanges);
            else
                await Create(entity, SaveChanges);

        }

        public async Task Create(TEntity entity, bool SaveChanges = true)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            if (SaveChanges)
                await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity, bool SaveChanges = true)
        {
            _dbContext.Set<TEntity>().Update(entity);
            if (SaveChanges)
                await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id, bool SaveChanges = true)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);

            if(SaveChanges)
                await _dbContext.SaveChangesAsync();
        }

    }




}
