using System.Web;
using System.Web.Optimization;

namespace Library.Apps
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //user js
            bundles.Add(new ScriptBundle("~/bundles/User").Include(
                      "~/Scripts/Pages/User/user.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //datatable css
            bundles.Add(new StyleBundle("~/Content/datatable-css").Include(
                      "~/Content/dataTables.bootstrap.min.css"));

            //datatable js
            bundles.Add(new ScriptBundle("~/bundles/datatable-js").Include(
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.bootstrap.min.js"));

            //list users js
            bundles.Add(new ScriptBundle("~/bundles/user-list-js").Include(
                      "~/Scripts/Pages/User/user-list.js"));

            //book js
            bundles.Add(new ScriptBundle("~/bundles/book-js").Include(
                      "~/Scripts/Pages/Book/book-js.js"));

            //book js
            bundles.Add(new ScriptBundle("~/bundles/book-transaction-js").Include(
                      "~/Scripts/Pages/Book/book-transaction.js"));
            
            //common js
            bundles.Add(new ScriptBundle("~/bundles/common-js").Include(
                      "~/Scripts/common-js.js"));
        }
    }
}
