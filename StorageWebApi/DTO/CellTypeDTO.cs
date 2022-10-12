using System.ComponentModel.DataAnnotations;

namespace Storage.WebApi.DTO
{
    public class CellTypeDTO : IBaseDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [Range(0, 10000)]
        public double MaxWeight { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Length { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Width { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Height { get; set; }
    }

    public class CellTypeReadOnlyInfoDTO : IBaseDTO
    {
        public int Id { get; init; }

        [Required]
        [StringLength(25)]
        public string Name { get; init; }

        [Required]
        [Range(0, 10000)]
        public double MaxWeight { get; init; }

        [Required]
        [Range(0, 10000)]
        public int Length { get; init; }

        [Required]
        [Range(0, 10000)]
        public int Width { get; init; }

        [Required]
        [Range(0, 10000)]
        public int Height { get; init; }

        public int CellsCount
        {
            get
            {
                if (SubAreas == null)
                    return 0;

                return SubAreas.Count();
            }
        }

        public IEnumerable<SubAreaDTOReadOnlyInfo> SubAreas { get; init; }
    }
}
