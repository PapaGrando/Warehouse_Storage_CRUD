using System.ComponentModel.DataAnnotations;

namespace Storage.Core.Models.Storage
{
    public class StorageItemState
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Состояние")]
        [MaxLength(50)]
        [MinLength(3)]
        [Required]
        public string Description { get; set; }

        public List<StorageItem> Items { get; set; }
    }
}
