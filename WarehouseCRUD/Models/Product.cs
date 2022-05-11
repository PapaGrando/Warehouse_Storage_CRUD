﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseCRUD.Storage.Models.Storage;

namespace WarehouseCRUD.Storage.Models
{
    public class Product
    {
        public readonly string DefailtImageUrl = "~/images/defaultprod.png";

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно")]
        [StringLength(50, ErrorMessage = "Длинное название")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно"), Display(Name = "Цена ₽")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Обязательно")]
        [Range(0, double.MaxValue, ErrorMessage = "Неккоректное значение")]
        [Display(Name = "Вес кг")]
        public double Weight { get; set; }

        [Display(Name = "Изображение")]
        [StringLength(250, ErrorMessage = "Слишком длинная ссылка")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Обязательно")]
        [Range(0, int.MaxValue, ErrorMessage = "Неккоректное значение")]
        [Display(Name = "Длина см")]
        public int Length { get; set; }
        [Required(ErrorMessage = "Обязательно")]
        [Range(0, int.MaxValue, ErrorMessage = "Неккоректное значение")]
        [Display(Name = "Ширина см")]
        public int Width { get; set; }
        [Required(ErrorMessage = "Обязательно")]
        [Range(0, int.MaxValue, ErrorMessage = "Неккоректное значение")]
        [Display(Name = "Высота см")]
        public int Height { get; set; }
        public int? ProductCategoryId { get; set; }
        [ForeignKey("ProductCategoryId")]
        [DisplayFormat(NullDisplayText = "Не определено")]
        [Display(Name = "Категория товара")]
        public virtual ProductCategory ProductCategory { get; set; }

        public List<StorageItem> Items { get; set; }
    }
}
