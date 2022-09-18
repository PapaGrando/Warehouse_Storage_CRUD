using System.Threading.Tasks;

namespace WarehouseCRUD.Storage.Sevices.Interfaces
{
    public interface IRazorRenderService
    {
        Task<string> ToStringAsync<T>(string viewName, T model);
    }
}
