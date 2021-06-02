using System.ComponentModel.DataAnnotations;

namespace BRD_Sem.Models.ViewModels.ProfileModels
{
    public class PasswordChangeModel
    {
        [Required(ErrorMessage = "Не указан старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        
        [Required(ErrorMessage = "Не указан новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Required(ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
    }
}