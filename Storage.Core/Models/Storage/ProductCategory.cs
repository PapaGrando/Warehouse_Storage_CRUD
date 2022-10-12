using Storage.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Core.Models.Storage
{
    public class ProductCategory : IBaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "ProductCategory name is Required")]
        [MaxLength(25)]
        [MinLength(3)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}