using Storage.Core.Models;

namespace Storage.Core.Interfaces
{
    public interface IProductCategoryRepoAsync : IBaseRepoAsync<ProductCategory> 
    {
        Task<bool> IsContainsProductInCategoryAsync(int id);
    }
}
