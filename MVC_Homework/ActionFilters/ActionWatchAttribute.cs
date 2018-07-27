using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Homework.ActionFilters
{
    public class ActionWatchAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch actionWatch = new Stopwatch();
        private readonly Stopwatch resultWatch = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            actionWatch.Restart();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            actionWatch.Stop();
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            Debug.WriteLine($"[INFO] {controllerName} {actionName} 執行Action所花時間: {actionWatch.Elapsed.ToString()}");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            resultWatch.Restart();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            resultWatch.Stop();

            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            Debug.WriteLine($"[INFO] {controllerName} {actionName} ActionResult所花時間: {actionWatch.Elapsed.ToString()}");
        }
    }
}