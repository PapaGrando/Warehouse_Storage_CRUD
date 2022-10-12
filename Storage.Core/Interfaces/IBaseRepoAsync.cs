using Storage.Core.Models;

namespace Storage.Core.Interfaces
{
    public interface IBaseRepoAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetSelectedAsync(QuerySettings query);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
