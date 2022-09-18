using Storage.Core.DTO;
using Storage.Core.Models.Storage;

namespace Storage.Core.Interfaces
{
    public interface IAreaRepoAsync : IBaseRepoAsync<Area>
    {
        Task<IList<AreaInfo>> GetAreasInfoAsync();
        Task<bool> IsContainsStorageItemsInArea(int areaId);
    }
}
