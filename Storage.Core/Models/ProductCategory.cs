using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Core.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно")]
        [MaxLength(25)]
        [MinLength(3)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}