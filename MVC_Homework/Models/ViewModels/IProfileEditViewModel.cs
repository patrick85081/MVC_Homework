using System.ComponentModel.DataAnnotations;

namespace MVC_Homework.Models.ViewModels
{
    public interface IProfileEditViewModel
    {

        [Required]
        int Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        string 電話 { get; set; }
        string 傳真 { get; set; }
        string Email { get; set; }
        string 地址 { get; set; }
        string 密碼 { get; set; }
        string 確認密碼 { get; set; }
    }
}