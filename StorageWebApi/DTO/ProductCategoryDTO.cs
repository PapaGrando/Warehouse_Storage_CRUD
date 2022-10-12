using Storage.Core.Models.Storage;
using System.ComponentModel.DataAnnotations;

namespace Storage.WebApi.DTO
{
    public class ProductCategoryDTO : IBaseDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string Name { get; set; }
    }

    public class ProductCategoryInfoReadOnly : IBaseDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public int ProductsCount
        {
            get
            {
                if (Products == null)
                    return 0;

                return Products.Count();
            }
        }

        public IEnumerable<ProductDTO> Products { get; init; }
    }
}
