using System.ComponentModel.DataAnnotations;

namespace BRD_Sem.Models.ViewModels.AccountModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Не указан Email")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Не указан Email")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
        
        public string RememberMe { get; set; }
    }
}