using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class CellTypeRepoAsync : BaseRepoAsync<CellType>, ICellTypeRepoAsync
    {
        private StorageDbContext _context;

        public CellTypeRepoAsync(StorageDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsCountainsCellsOfTypes(int id) =>
            await Task.FromResult(_context.Cells.Any(x => x.CellTypeId == id));

    }
}
