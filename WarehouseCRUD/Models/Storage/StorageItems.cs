using System.ComponentModel.DataAnnotations;

namespace WarehouseCRUD.Storage.Models.Storage
{
    public class StorageItems
    {
        [Key]
        public int Id { get; set; }
        public Product InfoId { get; set; }
        public Cell CellId { get; set; }
    }
}
