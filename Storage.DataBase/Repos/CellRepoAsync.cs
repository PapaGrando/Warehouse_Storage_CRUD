using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class CellRepoAsync : BaseRepoAsync<Cell>, ICellRepoAsync
    {
        public CellRepoAsync(StorageDbContext context) : base(context)
        {
        }
    }
}
