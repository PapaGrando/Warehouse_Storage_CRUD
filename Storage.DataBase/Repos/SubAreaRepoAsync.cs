using Microsoft.Extensions.Logging;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace Storage.DataBase.Repos
{
    public class SubAreaRepoAsync : BaseRepoAsync<SubArea>, ISubAreasRepoAsync
    {
        public SubAreaRepoAsync(StorageDbContext context, ILogger<SubAreaRepoAsync> logger) : base(context, logger)
        {
        }
    }
}
