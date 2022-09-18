using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Core.Models.Storage
{
    public class SubArea
    {
        [Key]
        public int Id { get; set; }

        public int NoOfSubArea { get; set; }

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

        public List<Cell> Cells { get; set; }
    }
}
