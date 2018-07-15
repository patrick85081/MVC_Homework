using System.Web;
using System.Web.Mvc;
using MVC_Homework1.ActionFilters;

namespace MVC_Homework1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ActionWatchAttribute());
        }
    }
}
