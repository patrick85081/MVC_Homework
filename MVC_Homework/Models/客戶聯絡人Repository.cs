using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Homework.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
	    public override IQueryable<客戶聯絡人> All()
	    {
	        return base.All().Where(concat => !concat.已刪除 && !concat.客戶資料.已刪除);
	    }

        public 客戶聯絡人 Find(int? id) =>
	        id.HasValue ? 
	            this.All().FirstOrDefault(concat => concat.Id == id.Value) : 
	            null;

	    public IQueryable<客戶聯絡人> Search(string keyword) =>
	        string.IsNullOrEmpty(keyword) ? 
	            this.All() : 
	            this.All().Where(concat => concat.姓名.Contains(keyword));

	    public IQueryable<客戶聯絡人> Search(string keyword, string job) =>
	        Search(keyword)
	            .Where(concat => string.IsNullOrEmpty(job) || concat.職稱 == job);

	    public IQueryable<客戶聯絡人> Search(string keyword, string job, string 客戶名稱) =>
	        Search(keyword, job)
	            .Where(concat => string.IsNullOrEmpty(客戶名稱) || concat.客戶資料.客戶名稱 == 客戶名稱);

        public override void Delete(客戶聯絡人 entity)
	    {
	        entity.已刪除 = true;
	    }
    }

    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {
        客戶聯絡人 Find(int? id);
        IQueryable<客戶聯絡人> Search(string keyword);
        IQueryable<客戶聯絡人> Search(string keyword, string job);
        IQueryable<客戶聯絡人> Search(string keyword, string job, string 客戶名稱);
    }
}