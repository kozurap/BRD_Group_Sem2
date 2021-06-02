using System.ComponentModel.DataAnnotations;

namespace BRD_Sem.Models.ViewModels.AccountModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string RememberMe { get; set; }
    }
}