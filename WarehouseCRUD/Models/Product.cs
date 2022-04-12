using System.ComponentModel.DataAnnotations;

namespace WarehouseCRUD.Storage.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно")]
        [StringLength(50, ErrorMessage = "Длинное название")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно"), Display(Name = "Цена ₽")]
        [Range(0.0d, double.MaxValue, ErrorMessage = "Некоректная цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Обязательно")]
        [Range(0.0f, float.MaxValue, ErrorMessage = "Неккоректное значение")]
        [Display(Name = "Вес кг")]
        public float Weight { get; set; }

        [Display(Name = "Изображение")]
        [StringLength(250, ErrorMessage = "Слишком длинная ссылка")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Обязательно")]
        [Range(0.0, int.MaxValue, ErrorMessage = "Неккоректное значение")]
        [Display(Name = "Длина см")]
        public int Length { get; set; }
        [Required(ErrorMessage = "Обязательно")]
        [Range(0.0, int.MaxValue, ErrorMessage = "Неккоректное значение")]
        [Display(Name = "Ширина см")]
        public int Width { get; set; }
        [Required(ErrorMessage = "Обязательно")]
        [Range(0.0, int.MaxValue, ErrorMessage = "Неккоректное значение")]
        [Display(Name = "Высота см")]
        public int Height { get; set; }
    }
}
