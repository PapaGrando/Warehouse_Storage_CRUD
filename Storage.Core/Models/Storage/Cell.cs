using Storage.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Core.Models.Storage
{
    public class Cell : IBaseModel
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public string Name { get => $"{SubAreaId}||{SubAreaLengthX}-{SubAreaHeigthZ}-{SubAreaWidthY}"; }
        public int SubAreaId { get; set; }
        [Required]
        [ForeignKey("SubAreaId")]
        public SubArea SubArea { get; set; }

        //Расположение ячейки в SubArea
        public int SubAreaLengthX { get; }
        public int SubAreaWidthY { get; }
        public int SubAreaHeigthZ { get; }

        public Cell(int subAreaLengthX, int subAreaWidthY, int subAreaHeigthZ)
        {
            SubAreaLengthX = subAreaLengthX;
            SubAreaWidthY = subAreaWidthY;
            SubAreaHeigthZ = subAreaHeigthZ;
        }

        public List<StorageItem> Items { get; set; }
    }
}
