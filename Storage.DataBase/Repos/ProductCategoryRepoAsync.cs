using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.DataBase.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Storage.DataBase.Repos
{
    public class ProductCategoryRepoAsync : BaseRepoAsync<ProductCategory>, IProductCategoryRepoAsync
    {
        private StorageDbContext _context;

        public ProductCategoryRepoAsync(StorageDbContext context) : base(context)
        {
            _context = context;
        }

        //TODO : Переопределить Delete и проверять там
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> IsContainsProductInCategoryAsync(int idCategory)
        {
            var tableName = "\"Products\"";
            var res = await _context.ProductCategories.FromSqlInterpolated($"SELECT * FROM {tableName}").ToListAsync();


            var targetCat = _context.ProductCategories.AsQueryable().Where(x => x.Name[0] == 'M')
                .First();

            if (targetCat is null)
                throw new ArgumentException($"Category with this {nameof(idCategory)} does not exist");

            var result = targetCat.Products.AsQueryable().Any();

            return result;
        }
    }
}
