using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseCRUD.Storage.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}