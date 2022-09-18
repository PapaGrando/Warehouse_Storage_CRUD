using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class ProductRepoAsync : BaseRepoAsync<Product>, IProductRepoAsync
    {
        public ProductRepoAsync(StorageDbContext context) : base(context)
        {

        }
    }
}
