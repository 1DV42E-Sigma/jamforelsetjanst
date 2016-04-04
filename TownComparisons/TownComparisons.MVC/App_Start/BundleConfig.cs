using System.Web;
using System.Web.Optimization;

namespace TownComparisons.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //Add angular as bundle
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js", 
                        "~/Scripts/angular-cookies.js",
                        "~/Scripts/angular-aria.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css",
                        "~/Content/Foundations-icon.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular-google-maps").Include(
                        "~/Scripts/angular-google-maps*",
                        "~/Scripts/lodash.js",
                        "~/Scripts/angular-simple-logger.js"));

            #region Foundation Bundles

            bundles.Add(Foundation.Scripts());
            #endregion


            bundles.Add(new ScriptBundle("~/bundles/mm-foundation").Include(
                        "~/Scripts/mm-foundation-tpls-*"));
            
            bundles.Add(new ScriptBundle("~/bundles/ng-file-upload").Include(
                        "~/Scripts/ng-file-upload-shim*",
                        "~/Scripts/ng-file-upload*"));



        }
    }
}
