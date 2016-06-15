using System.Web;
using System.Web.Optimization;

namespace WebPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Content/jquery.backstretch.js",
                        "~/Scripts/jquery-jvectormap-1.2.2.min.js",
                        "~/Scripts/jquery-jvectormap-world-mill-en.js",
                        "~/Scripts/jquery.knob.js",
                        "~/Scripts/jquery.slimscroll.min.js",
                        "~/Scripts/jquery.sparkline.min.js",
                        "~/Content/scripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap3-wysihtml5.all.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/js").Include(
                      "~/Scripts/app.min.js",
                      "~/Scripts/dashboard.js",
                      "~/Scripts/daterangepicker.js",
                      "~/Scripts/demo.js",
                      "~/Scripts/fastclick.min.js",
                      "~/Scripts/morris.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/hover.css",
                      "~/Content/_all-skins.min.css",
                      "~/Content/AdminLTE.min.css",
                      "~/Content/blue.css",
                      "~/Content/datepicker3.css",
                      "~/Content/daterangepicker-bs3.css",
                      "~/Content/jquery-jvectormap-1.2.2.css",
                      "~/Content/morris.css",
                      "~/Content/bootstrap3-wysihtml5.min.css",
                      "~/Content/Site.css",
                      "~/Content/style.css"
                       ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
