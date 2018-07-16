using System.Web;
using System.Web.Optimization;

namespace MVC_Homework1
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));
                //"~/Scripts/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/morris").Include(
                "~/Scripts/plugins/morris/raphael.min.js",
                "~/Scripts/plugins/morris/morris.min.js",
                "~/Scripts/plugins/morris/morris-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/viewModels/CustomerInfomationViewModel.js"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      //"~/Content/site.css",
                      "~/Content/sb-admin.css"));

            bundles.Add(new StyleBundle("~/bundles/morriscss").Include(
                    "~/Content/plugins/morris.css"));

            bundles.Add(new StyleBundle("~/bundles/css/font-awesome").Include(
                    "~/font-awesome/css/font-awesome.css"));
        }
    }
}
