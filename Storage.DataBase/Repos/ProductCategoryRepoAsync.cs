﻿using Storage.Core.Interfaces;
using Storage.DataBase.DataContext;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Models.Storage;
using Storage.DataBase.Exceptions;
using Microsoft.Extensions.Logging;

namespace Storage.DataBase.Repos
{
    public class ProductCategoryRepoAsync : BaseRepoAsync<ProductCategory>, IProductCategoryRepoAsync
    {
        private StorageDbContext _context;
        private ILogger _logger;

        public ProductCategoryRepoAsync(StorageDbContext context, ILogger<ProductCategoryRepoAsync> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async override Task<ProductCategory> GetByIdAsync(int id) =>
            await _context.ProductCategories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);

        /// <exception cref="NoCascadeDeletionException"></exception>
        public override Task DeleteAsync(ProductCategory category)
        {
            _logger.LogInformation("Checking for cascade deletion...");

            if (_context.Products.Any(x => x.ProductCategoryId == category.Id))
                throw new NoCascadeDeletionException<ProductCategory>();

            return base.DeleteAsync(category);         
        }
    }
}
