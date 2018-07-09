using System.ComponentModel;
using System.Linq;
using MVC_Homework1.Models.Validations;
using MVC_Homework1.ViewModels;

namespace MVC_Homework1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            I客戶資料Repository customeRepository = RepositoryHelper.Get客戶資料Repository();

            var customer = customeRepository.Find(this.客戶Id);
            if (customer != null)
            {
                var isRepeat = customer.Email == this.Email ||
                               (from concat in customer.客戶聯絡人
                                   where concat.Id != this.Id
                                   select concat.Email)
                               .Any(mail => mail == this.Email);

                if (isRepeat)
                    yield return new ValidationResult("Email 已經重複", new[] {nameof(Email)});
            }

            customeRepository.UnitOfWork.Context.Dispose();
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [EmailAddress]
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }
        
        [CellPhoneValdation]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        [ExcelIgnore]
        public bool 已刪除 { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
