using Storage.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Core.Models.Storage
{
    public class SubArea : IBaseModel
    {
        [Key]
        public int Id { get; set; }

        //Кол-во ячеек в высоту, длину, и ширину (В ширину макс 2, тк будет невозможно брать товар в центре)
        [Range(1, int.MaxValue)]
        public int HeightCells { get; set; }
        [Range(1, int.MaxValue)]
        public int LengthCells { get; set; }
        [Range(1, 2)]
        public int WidthCells { get; set; }

        public int AreaId { get; set; }
        [Required]
        [ForeignKey("AreaId")]
        public Area Area { get; set; }
        public int CellTypeId { get; set; }
        [Required]
        [ForeignKey("CellTypeId")]
        public CellType CellType { get; set; }
        public IEnumerable<Cell> Cells { get; set; }
    }
}
