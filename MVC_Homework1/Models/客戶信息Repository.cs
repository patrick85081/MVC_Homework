using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Homework1.Models
{   
	public  class 客戶信息Repository : EFRepository<客戶信息>, I客戶信息Repository
	{
	    public IQueryable<客戶信息> Search(string keyword) =>
	        string.IsNullOrEmpty(keyword) ?
	            this.All() :
	            this.All().Where(customer => customer.客戶名稱.Contains(keyword));
    }

	public  interface I客戶信息Repository : IRepository<客戶信息>
	{
	    IQueryable<客戶信息> Search(string keyword);
	}
}