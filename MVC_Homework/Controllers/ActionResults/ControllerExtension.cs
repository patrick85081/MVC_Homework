using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC_Homework.Controllers.ActionResults
{
    public static class ControllerExtension
    {
        public static ActionResult ExcelFile<T>(this Controller controller, IEnumerable<T> sources,string fileName = "report.xlsx")
        {
            var modelType = typeof(T);
           
            return new ExcelFileResult<T> {FileName = fileName, Models = sources};
        }
    }
}