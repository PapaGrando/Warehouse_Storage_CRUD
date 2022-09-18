using Storage.Core.Models.Storage;
namespace Storage.Core.Interfaces
{
    public interface ICellTypeRepoAsync : IBaseRepoAsync<CellType> 
    {
        Task<bool> IsCountainsCellsOfTypes(int id);
    }
}
