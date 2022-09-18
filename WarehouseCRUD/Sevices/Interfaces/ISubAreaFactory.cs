using Storage.Core.DTO;
using Storage.Core.Models.Storage;

namespace WarehouseCRUD.Storage.Sevices.Interfaces
{
    public interface ISubAreaFactory
    {
        SubArea Create(SubAreaConfiguration config);
    }
}
