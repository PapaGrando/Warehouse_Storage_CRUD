using Storage.Core.Models.Storage;
using System.ComponentModel.DataAnnotations;

namespace Storage.Core.DTO
{
    public class SubAreaConfiguration
    {
        [Range(1, int.MaxValue)]
        public int SubAreaNo { get; set; }

        [Display(Name = "Длина зоны в ячейках")]
        [Range(1, 1000)]
        public int Length { get; set; }

        [Display(Name = "Ширина зоны в ячейках (макс 2)")]
        [Range(1, 2)]
        public int Width { get; set; }

        [Display(Name = "Высота зоны в ячейках")]
        [Range(1, 85)]
        public int Height { get; set; }

        [Display(Name = "Тип ячеек в зоне")]
        public int? CellTypeId { get; set; }
        [Display(Name = "Тип ячеек в зоне")]
        public CellType CellType { get; set; }
    }
}
