using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Homework1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
	    public 客戶聯絡人 Find(int? id) =>
	        id.HasValue ? 
	            this.All().FirstOrDefault(concat => concat.Id == id.Value) : 
	            null;

	    public IQueryable<客戶聯絡人> Search(string keyword) =>
	        string.IsNullOrEmpty(keyword) ? 
	            this.All() : 
	            this.All().Where(concat => concat.姓名.Contains(keyword));

	    public override void Delete(客戶聯絡人 entity)
	    {
	        entity.已刪除 = true;
	    }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {
        客戶聯絡人 Find(int? id);
        IQueryable<客戶聯絡人> Search(string keyword);
    }
}