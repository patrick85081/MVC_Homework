using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC_Homework1.Models
{   
	public  class 客戶信息Repository : EFRepository<客戶信息>, I客戶信息Repository
	{

	}

	public  interface I客戶信息Repository : IRepository<客戶信息>
	{

	}
}