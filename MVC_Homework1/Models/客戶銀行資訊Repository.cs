using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Homework1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
	    public 客戶銀行資訊 Find(int? id) =>
	        id.HasValue ?
	            this.All().FirstOrDefault(blank => blank.Id == id.Value) :
	            null;

	    public IQueryable<客戶銀行資訊> Search(string keyword) =>
	        string.IsNullOrEmpty(keyword) ?
	            this.All() :
	            this.All().Where(blank => blank.銀行名稱.Contains(keyword));
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{
	    客戶銀行資訊 Find(int? id);
	    IQueryable<客戶銀行資訊> Search(string keyword);
	}
}