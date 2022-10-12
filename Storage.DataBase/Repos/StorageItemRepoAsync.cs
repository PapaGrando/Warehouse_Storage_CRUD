using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class StorageItemRepoAsync : BaseRepoAsync<StorageItem>, IStorageItemRepoAsync
    {
        private readonly StorageDbContext _context;

        public StorageItemRepoAsync(StorageDbContext context, ILogger<StorageItemRepoAsync> logger)
            : base(context, logger)
        {
            _context = context;
        }

        public override Task<StorageItem> AddAsync(StorageItem entity)
        {
            return base.AddAsync(entity);
        }

        public override async Task<StorageItem> GetByIdAsync(int id) =>
            await _context.AllItems
                .Include(x => x.Product)
                .ThenInclude(t => t.ProductCategory)
                .Include(x => x.Cell).FirstOrDefaultAsync(x => x.Id == id);

        public override Task UpdateAsync(StorageItem entity)
        {
            return base.UpdateAsync(entity);
        }
    }
}
