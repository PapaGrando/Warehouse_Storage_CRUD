using System.ComponentModel.DataAnnotations;

namespace WarehouseCRUD.Storage.Models.Storage
{
    public class StorageItems
    {
        [Key]
        public int Id { get; set; }
        public Product InfoId { get; set; }
        public Cell Cell { get; set; }
        public StorageState State { get; set; }
    }

    public enum StorageState
    {
        [Display(Name = "Не расположен")]
        None,

        [Display(Name = "Ожидает приема")]
        Arrival,

        [Display(Name = "В хранилище")]
        OnCell,

        [Display(Name = "В отгрузке")]
        OnShipment
    }
}
