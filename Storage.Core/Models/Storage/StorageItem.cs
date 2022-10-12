using Storage.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Core.Models.Storage
{
    public class StorageItem : IBaseModel
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int? CellId { get; set; }
        [ForeignKey("CellId")]
        public Cell Cell { get; set; }

        public DateTime AddTime { get; set; }   
    }
}
