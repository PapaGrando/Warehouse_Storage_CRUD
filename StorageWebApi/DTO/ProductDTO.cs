using System.ComponentModel.DataAnnotations;

namespace Storage.WebApi.DTO
{
    public class ProductDTO : IBaseDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required()]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Length { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Width { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Height { get; set; }
        public int? ProductCategoryId { get; set; }
    }

    public class ProductDTOInfoReadOnly : IBaseDTO
    {
        public int Id { get; init; }

        [Required]
        [StringLength(50)]
        public string Name { get; init; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; init; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Weight { get; init; }

        [StringLength(250)]
        public string ImageUrl { get; init; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Length { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Width { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Height { get; init; }
        public ProductCategoryDTO ProductCategory { get; init; }

        public int ItemsCount { get; init; }

        public IEnumerable<StorageItemDTO>? Items { get; init; }
    }

    public class ProductDTOItemInfoReadOnly : IBaseDTO
    {
        public int Id { get; init; }

        [Required]
        [StringLength(50)]
        public string Name { get; init; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; init; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Weight { get; init; }

        [StringLength(250)]
        public string ImageUrl { get; init; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Length { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Width { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Height { get; init; }
        public ProductCategoryDTO ProductCategory { get; init; }
    }
}
