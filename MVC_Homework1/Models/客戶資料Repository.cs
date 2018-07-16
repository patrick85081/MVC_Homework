using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Homework1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
	    public override IQueryable<客戶資料> All()
	    {
	        return base.All().Where(customer => !customer.已刪除);
	    }

	    public 客戶資料 Find(int? id) =>
	        id.HasValue ?
	            this.All().FirstOrDefault(customer => customer.Id == id.Value) :
	            null;

	    public IQueryable<客戶資料> Search(string keyword) =>
	        string.IsNullOrEmpty(keyword) ?
	            this.All() :
	            this.All().Where(customer => customer.客戶名稱.Contains(keyword));

	    public IQueryable<客戶資料> Search(string keyword, string category) =>
	        Search(keyword)
	            .Where(customer => string.IsNullOrEmpty(category) || category == customer.客戶分類);

        public IQueryable<string> Get客戶分類() =>
	        this.All().Select(customer => customer.客戶分類).Distinct().DefaultIfEmpty("");

	    public 客戶資料 Login(string account, string password)
	    {
	        return this.All().FirstOrDefault(customer => customer.帳號 == account &&
	                                                     customer.密碼 == password);
	    }

        public override void Delete(客戶資料 entity)
	    {
	        entity.已刪除 = true;
	    }
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{
	    客戶資料 Find(int? id);
	    IQueryable<客戶資料> Search(string keyword);
	    IQueryable<string> Get客戶分類();
	    IQueryable<客戶資料> Search(string keyword, string category);
	    客戶資料 Login(string account, string password);
    }
}