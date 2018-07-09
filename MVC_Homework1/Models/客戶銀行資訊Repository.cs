using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Homework1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
	    public override IQueryable<客戶銀行資訊> All()
	    {
	        return base.All().Where(blank => !blank.已刪除 && !blank.客戶資料.已刪除);
	    }

        public 客戶銀行資訊 Find(int? id) =>
	        id.HasValue ?
	            this.All().FirstOrDefault(blank => blank.Id == id.Value) :
	            null;

	    public IQueryable<客戶銀行資訊> Search(string keyword) =>
	        string.IsNullOrEmpty(keyword) ?
	            this.All() :
	            this.All().Where(blank => blank.銀行名稱.Contains(keyword));

	    public override void Delete(客戶銀行資訊 entity)
	    {
	        entity.已刪除 = true;
	    }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{
	    客戶銀行資訊 Find(int? id);
	    IQueryable<客戶銀行資訊> Search(string keyword);
	}
}