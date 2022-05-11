using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseCRUD.Storage.Models.Storage
{
    public class Cell
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        public string Name { get; }

        public int SubAreaId { get; set; }
        [ForeignKey("SubAreaId")]
        public SubArea SubArea { get; set; }

        public int AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Area Area { get; set; }

        //Расположение ячейки в SubArea
        public int SubAreaLenghtX { get; }
        public int SubAreaHeightY { get; }
        public int SubAreaWidthZ { get; }

        public int CellTypeId { get; set; }
        [ForeignKey("CellTypeId")]
        public CellType CellType { get; set; }

        public List<CellType> CellTypes { get; set; }
        public List<StorageItem> Items { get; set; }

        public Cell(string name, int subAreaLenghtX, int subAreaHeightY, int subAreaWidthZ)
        {
            Name = name;
            SubAreaLenghtX = subAreaLenghtX;
            SubAreaHeightY = subAreaHeightY;
            SubAreaWidthZ = subAreaWidthZ;
        }
    }
}
