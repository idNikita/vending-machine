using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VendingMachine.Models {

    public class Product {
        public int ProductID { get; set; }

        
        [Required(ErrorMessage = "Введите название")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Путь к изображению")]
        public string ImageFilePath { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
        [Display(Name = "Остаток")]
        public int Count { get; set; }
    }
}
