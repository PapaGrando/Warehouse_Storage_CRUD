using Microsoft.Extensions.DependencyInjection;
using Storage.Core.Builders;
using Storage.Core.Interfaces;
using Storage.DataBase.Repos;

namespace WarehouseCRUD.Storage.Helpers
{
    internal static class Utils
    {
        internal static void AddRepoServices(this IServiceCollection services)
        {
            services.AddTransient<IAreaRepoAsync, AreasRepoAsync>();
            services.AddTransient<IProductCategoryRepoAsync, ProductCategoryRepoAsync>();
            services.AddTransient<IProductRepoAsync, ProductRepoAsync>();

            services.AddTransient<ISubAreasRepoAsync, SubAreaRepoAsync>();
            services.AddTransient<ICellRepoAsync, CellRepoAsync>();
            services.AddTransient<ICellTypeRepoAsync, CellTypeRepoAsync>();

            services.AddTransient<IStorageItemRepoAsync, StorageItemRepoAsync>();

            services.AddTransient<IUnitOfWorkAsync, UnitOfWork>();
        }

        internal static void AddBuilders(this IServiceCollection services)
        {
            services.AddTransient<IAreaBuilder, AreaBuilder>();
        }
    }
}
