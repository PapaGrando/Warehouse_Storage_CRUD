using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class CellRepoAsync : BaseRepoAsync<Cell>, ICellRepoAsync
    {
        private readonly StorageDbContext _context;

        public CellRepoAsync(StorageDbContext context, ILogger<CellRepoAsync> logger)
            : base(context, logger)
        {
            _context = context;
        }

        public async Task<IList<Cell>> GetAllInSubAreaAsync(int subAreaId) =>
            await _context.Cells.Where(x => x.SubAreaId == subAreaId)
                .ToListAsync();

        public override async Task<IList<Cell>> GetSelectedAsync(QuerySettings query)
        {
            var pQuery = query as QuerySettingsWithIdSubArea;

            if (pQuery is null)
                return await base.GetSelectedAsync(query);

            return await _context.Cells.Where(x => x.SubAreaId == pQuery.IdSubArea)
                .Skip(query.Offset)
                .Take(pQuery.PageSize)
                .ToListAsync();
        }
    }
}
