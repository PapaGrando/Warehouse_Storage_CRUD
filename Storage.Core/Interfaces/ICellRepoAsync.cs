using Storage.Core.Models.Storage;
namespace Storage.Core.Interfaces
{
    public interface ICellRepoAsync : IBaseRepoAsync<Cell> 
    {
        Task<IList<Cell>> GetAllInSubAreaAsync(int subAreaId);
    }
}
