using System.ComponentModel.DataAnnotations;

namespace Storage.Core.Interfaces
{
    public class AreaDTO : IBaseDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(1)]
        public string Name { get; set; }
    }

    public class AreaDTOInfoReadOnly : IBaseDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(1)]
        public string Name { get; init; }
        public IEnumerable<SubAreaDTOReadOnlyInfo> SubAreas { get; init; }
    }
}
