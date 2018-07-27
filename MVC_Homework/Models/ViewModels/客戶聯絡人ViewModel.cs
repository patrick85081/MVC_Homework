using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVC_Homework.Controllers.ActionResults;
using MVC_Homework.Models.Validations;

namespace MVC_Homework.Models.ViewModels
{
    public class 客戶聯絡人ViewModel
    {
        [DisplayName("客戶聯絡人編號")]
        [Required]
        public int Id { get; set; }

        public string 客戶名稱 { get; set; }

        [DisplayName("客戶編號")]
        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [EmailAddress]
        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }

        [CellPhoneValdation]
        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    }
}