using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Homework1.Models.ViewModels
{
    public class ProfileViewModel : IProfileEditViewModel
    {
        [DisplayName("客戶編號")]
        [Required]
        public int Id { get; set; }

        public string 帳號 { get; set; }
        public string 客戶名稱 { get; set; }
        public string 統一編號 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 電話 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 傳真 { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string 地址 { get; set; }

        [EmailAddress]
        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        public string Email { get; set; }

        [DisplayName("密碼")]
        [DataType(DataType.Password)]
        //[Required]
        public string 密碼 { get; set; }

        [DisplayName("確認密碼")]
        [DataType(DataType.Password)]
        //[Required]
        [Compare("密碼", ErrorMessage = "必須與密碼欄位相同")]
        public string 確認密碼 { get; set; }
    }
}