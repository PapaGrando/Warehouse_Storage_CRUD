using Storage.Core.Models.Storage;

namespace Storage.WebApi.DTO
{
    public class StorageItemDTO : IBaseDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CellId { get; set; }
        public DateTime AddTime { get; set; }
    }

    public class StorageItemDTOInfoReadOnly : IBaseDTO
    {
        public int Id { get; set; }
        public ProductDTOItemInfoReadOnly Product { get; init; }
        public CellDTOShortInfoReadOnly Cell { get; init; }
        public DateTime AddTime { get; set; }
    }
}
