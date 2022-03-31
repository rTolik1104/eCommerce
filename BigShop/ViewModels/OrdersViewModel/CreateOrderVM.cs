using System.ComponentModel.DataAnnotations;

namespace BigShop.ViewModels.OrdersViewModel
{
    public class CreateOrderVM
    {
        [Required, MinLength(5)]
        [Display(Name = "Введите ваше имя")]
        public string? UserName { get; set; }

        [Required, Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required, MinLength(5)]
        [Display(Name = "Ваш регион проживания")]
        public string? Region { get; set; }

        [Required, Display(Name = "Адресс"), MinLength(7)]
        public string? Address { get; set; }
        [Required,Display(Name ="Названия товара")]
        public string? ProductName { get; set; }

        [Required,Display(Name ="Количество")]
        public int? ProductCount { get; set; }
    }
}
