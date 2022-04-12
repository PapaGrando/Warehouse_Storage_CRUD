using System.ComponentModel.DataAnnotations;

namespace WarehouseCRUD.Storage.Models.Storage
{
    /// <summary>
    /// Тип ячейки, обозначающая, какой тип товаров можно хранить
    /// Мелкий, Ячейки мезонина, Крупногабаритный стеллаж, Напольные ячейки
    /// </summary>
    public class CellType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [Range(0.0f, float.MaxValue)]
        [Display(Name = "Вес кг")]
        public float MaxWeight { get; set; }

        [Required]
        [Range(0.0, int.MaxValue)]
        [Display(Name = "Длина см")]
        public int Length { get; set; }

        [Required]
        [Range(0.0, int.MaxValue)]
        [Display(Name = "Ширина см")]
        public int Width { get; set; }

        [Required]
        [Range(0.0, int.MaxValue)]
        [Display(Name = "Высота см")]
        public int Height { get; set; }
    }
}
