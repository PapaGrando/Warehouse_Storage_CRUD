using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class CellTypeRepoAsync : BaseRepoAsync<CellType>, ICellTypeRepoAsync
    {
        private StorageDbContext _context;

        public CellTypeRepoAsync(StorageDbContext context, ILogger<CellTypeRepoAsync> logger)
            : base(context, logger)
        {
            _context = context;
        }
    }
}
