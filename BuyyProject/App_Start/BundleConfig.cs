using System.Web;
using System.Web.Optimization;

namespace BuyyProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.6.0.slim.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/popper").Include(
            "~/Scripts/umd/popper.min.js"));


            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
            "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/theme").Include(
                "~/Content/theme-style.css", 
                "~/Content/font-awesome-4.7.0/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/holder").Include(
                "~/Scripts/holder.min.js"));


            //IonSlider:
            bundles.Add(new ScriptBundle("~/bundles/ionRangeSlider").Include(
                      "~/Scripts/ion.rangeSlider.min.js"));
            bundles.Add(new StyleBundle("~/Content/ionRangeSliderCss").Include(
                      "~/Content/ion.rangeSlider.min.css"));
        }
    }
}