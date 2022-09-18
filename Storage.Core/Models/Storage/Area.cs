using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Storage.Core.Models.Storage
{
    public class Area
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(1)]
        [Display(Name = "Название зоны")]
        public string Name { get; set; }
        public List<SubArea> SubAreas { get; set; }
        public List<Cell> Cells { get; set; }
        public List<StorageItem> StorageItems { get; set; }
    }
}
