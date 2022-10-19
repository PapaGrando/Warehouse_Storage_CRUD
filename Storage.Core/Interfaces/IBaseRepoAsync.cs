using Storage.Core.Interfaces;
using Storage.Core.Models;

namespace Storage.Core.Interfaces
{
    public interface IBaseRepoAsync<T> where T : class, IBaseModel
    {
        Task<T> GetByIdAsync(int id);
        Task<EntityListRepoData<T>> GetAllAsync();
        Task<EntityListRepoData<T>> GetSelectedAsync(QuerySettings query);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
