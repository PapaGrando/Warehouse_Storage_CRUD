using Microsoft.EntityFrameworkCore;
using Storage.Core.Interfaces;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class BaseRepoAsync<T> : IBaseRepoAsync<T> where T : class
    {
        private StorageDbContext _context;

        public BaseRepoAsync(StorageDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetByIdAsync(int id) =>
            await _context.Set<T>().FindAsync(id);

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IList<T>> GetAllAsync() =>
            await _context.Set<T>().ToListAsync();

    }
}
