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

        public async override Task<Cell> GetByIdAsync(int id) =>
            await _context.Cells
                .Include(x => x.SubArea).ThenInclude(x => x.CellType)
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<EntityListRepoData<Cell>> GetAllInSubAreaAsync(int subAreaId)
        {
            var data = await _context.Cells.Include(x => x.Items).Where(x => x.SubAreaId == subAreaId)
                .ToListAsync();

            return new EntityListRepoData<Cell>()
            {
                Entities = data,
                TotalCount = data.Count,
                CountInList = data.Count
            };
        }

        public override async Task<EntityListRepoData<Cell>> GetSelectedAsync(QuerySettings query)
        {
            var pQuery = query as QuerySettingsWithIdSubArea;

            if (pQuery is null)
                return await base.GetSelectedAsync(query);

            var totalCounts = await _context.Cells
                .Where(x => x.SubAreaId == pQuery.IdSubArea).CountAsync();

            var data = await _context.Cells.Include(x => x.Items).Where(x => x.SubAreaId == pQuery.IdSubArea)
                .Skip((query.PageNo - 1) * query.PageSize)
                .Take(pQuery.PageSize)
                .ToListAsync();

            return new EntityListRepoData<Cell>()
            {
                Entities = data,
                TotalCount = totalCounts,
                CountInList = data.Count
            };
        }
    }
}
