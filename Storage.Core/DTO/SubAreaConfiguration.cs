using System.ComponentModel.DataAnnotations;

namespace Storage.Core.Interfaces
{
    public class SubAreaConfiguration
    {
        [Range(1, 1000)]
        public int Length { get; set; }

        [Range(1, 2)]
        public int Width { get; set; }

        [Range(1, 85)]
        public int Height { get; set; }

        public int CellTypeId { get; set; }
    }
}
