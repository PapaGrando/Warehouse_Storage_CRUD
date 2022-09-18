using Storage.Core.DTO;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class AreasRepoAsync : BaseRepoAsync<Area>, IAreaRepoAsync
    {
        private StorageDbContext _context;
        public AreasRepoAsync(StorageDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<AreaInfo>> GetAreasInfoAsync()
        {
            var outData = _context.Areas.Select(x =>
                new AreaInfo(
                    x.Id,
                    x.Name,
                    x.SubAreas.Count(),
                    x.Cells.Count()))
                .ToList();

            return await Task.FromResult(outData);
        }

        public async Task<bool> IsContainsStorageItemsInArea(int areaId) =>
            await Task.FromResult(_context.Cells
                .Any(x => x.AreaId == areaId && x.Items.AsQueryable().Any()));
    }
}
