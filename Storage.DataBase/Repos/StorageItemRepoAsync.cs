using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    internal class StorageItemRepoAsync : BaseRepoAsync<StorageItem>, IStorageItemRepoAsync
    {
        public StorageItemRepoAsync(StorageDbContext context) : base(context)
        {
        }
    }
}
