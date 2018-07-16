using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Homework1.Utils
{
    public static class WebViewPageExtension
    {
        public static string GetCurrentController(this WebViewPage page) =>
            page.ViewContext.RouteData.Values["controller"].ToString();

        public static string GetCurrentAction(this WebViewPage page) =>
            page.ViewContext.RouteData.Values["action"].ToString();
    }
}