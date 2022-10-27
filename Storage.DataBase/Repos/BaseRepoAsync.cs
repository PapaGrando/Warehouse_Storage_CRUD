using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.DataBase.DataContext;
using Storage.DataBase.Exceptions;

namespace Storage.DataBase.Repos
{
    public class BaseRepoAsync<T> : IBaseRepoAsync<T> where T : class, IBaseModel
    {
        private StorageDbContext _context;
        private ILogger _logger;

        public BaseRepoAsync(StorageDbContext context, ILogger<BaseRepoAsync<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<T> GetByIdAsync(int id) =>
            await _context.Set<T>().FindAsync(id);

        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            _logger.LogInformation($"entity added");
            return entity;
        }

        public virtual Task UpdateAsync(T entity)
        {
            if (!IsExisting(entity))
                throw new NotFound(entity.Id, typeof(T).Name);

            _context.Entry(entity).State = EntityState.Modified;
            _logger.LogInformation($"entity state changed");

            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(T entity)
        {
            if (!IsExisting(entity))
                throw new NotFound(entity.Id, typeof(T).Name);

            _context.Set<T>().Remove(entity);
            _logger.LogInformation($"entity removed");

            return Task.CompletedTask;
        }

        /// <summary>
        /// returns ALL entities.  
        /// Use with care and wisely (Or call GetSelectedAsync)
        /// </summary>
        public virtual async Task<EntityListRepoData<T>> GetAllAsync()
        {
            var data = await _context.Set<T>().ToListAsync();

            return new EntityListRepoData<T>()
            {
                Entities = data,
                CountInList = data.Count,
                TotalCount = data.Count
            };
        }
            

        public virtual async Task<EntityListRepoData<T>> GetSelectedAsync(QuerySettings query)
        {
            var totalCounts = await _context.Set<T>().CountAsync();
            var data = await _context.Set<T>().Skip((query.PageNo - 1) * query.PageSize)
                                   .Take(query.PageSize).ToListAsync();

            return new EntityListRepoData<T>()
            {
                Entities = data,
                TotalCount = totalCounts,
                CountInList = data.Count
            };
        }
            
        protected virtual bool IsExisting(T entity)
        {
            _logger.LogInformation($"entity checking existing");

            return _context.Set<T>().Any(x => x.Id == entity.Id);
        }
    }
}
