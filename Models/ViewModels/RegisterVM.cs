using System.ComponentModel.DataAnnotations;

namespace ITReviewer.Libraries.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Це обов'язкове поле")]
        [MinLength(5, ErrorMessage = "Мінімальна довжина логіна - 5 символів")]
        [Display(Name = "Логін")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Це обов'язкове поле")]
        [Display(Name = "Пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Це обов'язкове поле")]
        [MinLength(6, ErrorMessage = "Мінімальна довжина паролю - 6 символів")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Це обов'язкове поле")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [Display(Name = "Підтвердіть Пароль")]
        public string PasswordConfirmed { get; set; }
    }
}
