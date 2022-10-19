using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Storage.Core.Builders;
using Storage.Core.Interfaces;
using Storage.DataBase.Exceptions;
using Storage.DataBase.Repos;
using Storage.WebApi.Helpers;

namespace WarehouseCRUD.Storage.Helpers
{
    internal static class DIUtils
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

        internal static void AddStorageServises(this IServiceCollection services)
        {
            services.AddTransient<IAreaBuilder, AreaBuilder>();
        }
    }
}
