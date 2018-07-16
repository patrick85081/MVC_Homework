using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Homework1.Models.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
        }

        [DisplayName("帳號")]
        [Required]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DisplayName("密碼確認")]
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "必須與密碼欄位相同")]
        public string PasswordConfirm { get; set; }
    }
}