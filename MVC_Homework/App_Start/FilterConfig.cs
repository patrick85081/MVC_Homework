using System.Web;
using System.Web.Mvc;
using MVC_Homework.ActionFilters;

namespace MVC_Homework
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
