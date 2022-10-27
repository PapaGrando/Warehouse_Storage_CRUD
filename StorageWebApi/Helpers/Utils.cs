using Microsoft.AspNetCore.Mvc;
using Storage.Core.Builders;
using Storage.Core.Interfaces;
using Storage.DataBase.Repos;

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

    internal static class ControllerUtils
    {
        internal static void AddHeadersToResponse<T>(this ControllerBase controller,
            params (string name, object value)[] parameters)
        {
            foreach (var p in parameters)

                controller.ControllerContext.HttpContext.Response.Headers
                    .Add($"x-{typeof(T).Name}-{p.name}", p.value.ToString());
        }
    }
}
