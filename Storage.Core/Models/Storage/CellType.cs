using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Storage.Core.Models.Storage
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
        [Display(Name = "Название типа")]
        public string Name { get; set; }

        [Required]
        [Range(0, 10000)]
        [Display(Name = "Максимальный вес кг")]
        public double MaxWeight { get; set; }

        [Required]
        [Range(0, 10000)]
        [Display(Name = "Длина см")]
        public int Length { get; set; }

        [Required]
        [Range(0, 10000)]
        [Display(Name = "Ширина см")]
        public int Width { get; set; }

        [Required]
        [Range(0, 10000)]
        [Display(Name = "Высота см")]
        public int Height { get; set; }

        public List<Cell> Cells { get; set; }
    }
}
