using System.ComponentModel.DataAnnotations;

namespace Storage.Core.DTO
{
    public class AreaConfiguration
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        [MinLength(1)]
        public string Name { get; set; }
        public IEnumerable<SubAreaConfiguration> SubAreasToCreate { get; set; }
    }
}
