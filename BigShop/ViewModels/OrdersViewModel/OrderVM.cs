using System.ComponentModel.DataAnnotations;

namespace BigShop.ViewModels.OrdersViewModel
{
    public class OrderVM
    {
        public Guid OrderId { get; set; }

        [Required, StringLength(5)]
        [Display(Name ="Введите ваше имя")]
        public string? UserName { get; set; }

        [Required,Display(Name ="Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required,StringLength(5)]
        [Display(Name ="Ваш регион проживания")]
        public string? Region { get; set; }

        [Required,Display(Name ="Адресс"), StringLength(10)]
        public string? Address { get; set; }

        public string? ProductName { get; set; }
        public int? ProductCount { get; set; }
        public string? CreatedDate { get; set; }
        public bool? IsComplete { get; set; }
    }
}
