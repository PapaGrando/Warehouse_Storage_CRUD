using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Core.Models.Storage
{
    public class Cell
    {
        [Key]
        public int Id { get; set; }

        public string Name { get => $"{SubAreaHeightY}-{SubAreaLenghtX}-{SubAreaWidthZ}"; }
        public int SubAreaId { get; set; }
        [Required]
        [ForeignKey("SubAreaId")]
        public SubArea SubArea { get; set; }
        public int AreaId { get; set; }
        [Required]
        [ForeignKey("AreaId")]
        public Area Area { get; set; }

        //Расположение ячейки в SubArea
        public int SubAreaLenghtX { get; init; }
        public int SubAreaHeightY { get; init; }
        public int SubAreaWidthZ { get; init; }

        public int? CellTypeId { get; set; }
        [ForeignKey("CellTypeId")]
        public CellType CellType { get; set; }

        public List<StorageItem> Items { get; set; }

        public Cell(int subAreaLenghtX, int subAreaHeightY, int subAreaWidthZ)
        {
            SubAreaLenghtX = subAreaLenghtX;
            SubAreaHeightY = subAreaHeightY;
            SubAreaWidthZ = subAreaWidthZ;
        }
    }
}
