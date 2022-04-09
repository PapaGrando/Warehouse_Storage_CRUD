using System.ComponentModel.DataAnnotations;

namespace WarehouseCRUD.Storage.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.0, int.MaxValue)]
        public int Weight { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        [Range(0.0, int.MaxValue)]
        public int Length { get; set; }
        [Required]
        [Range(0.0, int.MaxValue)]
        public int Width { get; set; }
        [Range(0.0, int.MaxValue)]
        public int Height { get; set; }
    }
}
