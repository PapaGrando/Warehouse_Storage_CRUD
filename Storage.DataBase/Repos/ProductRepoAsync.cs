using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class ProductRepoAsync : BaseRepoAsync<Product>, IProductRepoAsync
    {
        private readonly StorageDbContext _context;
        private readonly ILogger<ProductRepoAsync> _logger;

        public ProductRepoAsync(StorageDbContext context, ILogger<ProductRepoAsync> logger)
            : base(context, logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async override Task<Product> GetByIdAsync(int id) =>
            await _context.Products.Include(x => x.ProductCategory).FirstOrDefaultAsync(x => x.Id == id);
    }
}
