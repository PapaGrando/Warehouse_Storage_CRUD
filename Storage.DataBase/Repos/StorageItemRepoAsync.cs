using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;
using Storage.DataBase.Exceptions;

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

        public override async Task<StorageItem> GetByIdAsync(int id) =>
            await _context.AllItems
                .Include(x => x.Product)
                .ThenInclude(t => t.ProductCategory)
                .Include(x => x.Cell).FirstOrDefaultAsync(x => x.Id == id);

        public override async Task<StorageItem> AddAsync(StorageItem entity)
        {
            if (!await CheckItemFitToCell(entity))
                throw new StorageItemDoesNotFitInCell(entity.CellId, entity.ProductId);

            return await base.AddAsync(entity);
        }

        public async override Task UpdateAsync(StorageItem entity)
        {
            if (!await CheckItemFitToCell(entity))
                throw new StorageItemDoesNotFitInCell(entity.CellId, entity.ProductId);

            await base.UpdateAsync(entity);
        }

        protected async Task<bool> CheckItemFitToCell(StorageItem entity)
        {
            var result = await _context.StoredFunctionsResults
                .FromSqlInterpolated(
                $"SELECT storageitem_can_insert_in_cell({entity.CellId}, {entity.ProductId}, {entity.Id})")
                .FirstOrDefaultAsync();

            if (result == null)
                result = new StoredFunctionsResults() { storageitem_can_insert_in_cell = false };
                
            return result.storageitem_can_insert_in_cell;
        }
    }
}
