using Storage.Core.Models.Storage;
namespace Storage.Core.Interfaces
{
    public interface ICellRepoAsync : IBaseRepoAsync<Cell>
    {
        Task<EntityListRepoData<Cell>> GetAllInSubAreaAsync(int subAreaId);
    }
}
