using System.ComponentModel.DataAnnotations;

namespace BigShop.ViewModels.AccountViewModels
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Имя")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }
}
