using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Homework1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
	    public 客戶資料 Find(int? id) =>
	        id.HasValue ?
	            this.All().FirstOrDefault(customer => customer.Id == id.Value) :
	            null;

	    public IQueryable<客戶資料> Search(string keyword) =>
	        string.IsNullOrEmpty(keyword) ?
	            this.All() :
	            this.All().Where(customer => customer.客戶名稱.Contains(keyword));
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}