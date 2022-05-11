using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseCRUD.Storage.Models.Storage
{
    public class StorageItem
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int? CellId { get; set; }
        [ForeignKey("CellId")]
        public Cell Cell { get; set; }
        [Required]
        public StorageState State { get; set; }
    }

    public enum StorageState
    {
        [Display(Name = "Не расположен")]
        None = 0,

        [Display(Name = "Ожидает приема")]
        Arrival = 1,

        [Display(Name = "В хранилище")]
        OnCell = 2,

        [Display(Name = "В отгрузке")]
        OnShipment = 3
    }
}
