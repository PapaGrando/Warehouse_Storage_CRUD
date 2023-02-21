using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;
using Storage.DataBase.Exceptions;

namespace Storage.DataBase.Repos
{
    public class AreasRepoAsync : BaseRepoAsync<Area>, IAreaRepoAsync
    {
        private StorageDbContext _context;
        private readonly ILogger<AreasRepoAsync> _logger;

        public AreasRepoAsync(
            StorageDbContext context,
            ILogger<AreasRepoAsync> logger)
            : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        //full include (with cells and items in them)
        public override async Task<Area> GetByIdAsync(int id)
        {
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
                return area;

            var subAreas = await _context.SubAreas
                .Where(x => x.AreaId == id)
                .Include(x => x.CellType)
                .Include(x => x.Cells)
                .ThenInclude(x => x.Items)
                .ToListAsync();

            area.SubAreas = subAreas;
            return area;
        }

        public override async Task DeleteAsync(Area entity)
        {
            _logger.LogInformation("Checking for StorageItems in this Area...");

            var result = _context.SubAreas
                .Where(x => x.AreaId == entity.Id)
                .Include(x => x.CellType)
                .Include(x => x.Cells)
                .ThenInclude(x => x.Items)
                .Where(w => w.Cells.Where(x => x.Items.Any()).Any())
                .Any();

            if (result)
                throw new NoCascadeDeletionException(
                    $"Area with id = {entity.Id} contains StorageItems, " +
                    $"but must be empty. Replace or remove all items in this Area.");

            await base.DeleteAsync(entity);
        }
    }
}
