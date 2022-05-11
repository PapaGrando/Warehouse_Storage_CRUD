using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseCRUD.Storage.Models.Storage
{
    public class SubArea
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }

        //Кол-во ячеек в высоту, длину, и ширину (В ширину макс 2, тк будет невозможно брать товар в центре)
        [Range(0, int.MaxValue)]
        public int HeightCells { get; set; }
        [Range(0, int.MaxValue)]
        public int LengthCells { get; set; }
        [Range(0, 2)]
        public int WidthCells { get; set; }

        public int AreaId { get; set; }
        [Required]
        [ForeignKey("AreaId")]
        public Area Area { get; set; }

        public List<Cell> Cells { get; set; }
    }
}
