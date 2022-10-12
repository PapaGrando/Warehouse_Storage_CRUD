using Microsoft.Extensions.DependencyInjection;
using Storage.Core.Interfaces;
using Storage.DataBase.Repos;

namespace WarehouseCRUD.Storage.Helpers
{
    internal static class Utils
    {
        internal static void AddRepoServices(this IServiceCollection services)
        {
            services.AddTransient<IAreaRepoAsync, AreasRepoAsync>();
            services.AddTransient<IUnitOfWorkAsync, UnitOfWork>();
            services.AddTransient<IProductCategoryRepoAsync, ProductCategoryRepoAsync>();
            services.AddTransient<ICellTypeRepoAsync, CellTypeRepoAsync>();
        }
    }
}
