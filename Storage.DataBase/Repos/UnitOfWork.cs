using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class UnitOfWork : IUnitOfWorkAsync, IDisposable
    {
        private StorageDbContext _context;
        private Microsoft.Extensions.Logging.ILogger _logger;
        private bool disposedValue;

        public IAreaRepoAsync Areas { get; init; }
        public ISubAreasRepoAsync SubAreas { get; init; }
        public ICellRepoAsync Cells { get; init; }
        public ICellTypeRepoAsync CellTypes { get; init; }
        public IProductRepoAsync Products { get; init; }
        public IProductCategoryRepoAsync ProductCategories { get; init; }
        public IStorageItemRepoAsync StorageItems { get; init; }

        public UnitOfWork(StorageDbContext context,
            ILogger<UnitOfWork> logger,
            IAreaRepoAsync areas,
            ISubAreasRepoAsync subAreas,
            ICellRepoAsync cells,
            IProductRepoAsync products,
            ICellTypeRepoAsync cellTypes,
            IProductCategoryRepoAsync productCategories,
            IStorageItemRepoAsync storageItems)
        {
            _context = context;
            _logger = logger;
            Areas = areas;
            SubAreas = subAreas;
            Cells = cells;
            CellTypes = cellTypes;
            Products = products;
            ProductCategories = productCategories;
            StorageItems = storageItems;
        }

        public async Task Commit()
        {
            _logger.LogInformation($"commiting changes");

            try
            {
                await _context.SaveChangesAsync();

                _logger.LogInformation($"commited");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.ToString} - {ex.Message}\n {ex.StackTrace}");
                throw;
            }

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                _logger.LogInformation($"disposing {nameof(UnitOfWork)}");
                _context.Dispose();
            }


            disposedValue = true;
        }

        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
