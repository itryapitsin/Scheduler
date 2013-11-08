using System.Web;
using System.Web.Optimization;

namespace Timetable.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include("~/Scripts/jquery*"));
            bundles.Add(new ScriptBundle("~/Scripts/knockout").Include("~/Scripts/knockout*"));
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include("~/Scripts/modernizr*"));
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/bootstrap*"));
            bundles.Add(new ScriptBundle("~/Scripts/moment").Include("~/Scripts/moment*"));
            bundles.Add(new ScriptBundle("~/Scripts/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-cookies.js",
                "~/Scripts/angular-loader.js",
                "~/Scripts/angular-mask.js",
                "~/Scripts/angular-mocks.js",
                "~/Scripts/angular-promise-tracker.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-scenario.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-strap.js",
                "~/Scripts/angular-touch.js",
                "~/Scripts/angular-bootstrap.js",
                "~/Scripts/angular-bootstrap-prettify.js",
                "~/Scripts/angular-localstorage.js",
                "~/Scripts/ui-bootstrap-{version}.js",
                "~/Scripts/ui-bootstrap-tpls-{version}.js",
                "~/Scripts/i18n/angular-locale_ru-ru.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqwidgets").Include("~/Scripts/jqxcore.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Site/Index").Include("~/Scripts/Site/main.index.viewmodel.js"));

            bundles.Add(new ScriptBundle("~/Scripts/App").Include(
                "~/App/app.js",
                "~/App/directives/daterangepicker.js",
                "~/App/services/groupService.js",
                "~/App/Controllers/schedulerController.js"));

            bundles.Add(new StyleBundle("~/Content/Site").Include("~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/Content/jqwidgets").Include(
                "~/Content/jqwidgets/jqx.base.css", 
                "~/Content/jqwidgets/jqx.classic.css"));
            bundles.Add(new StyleBundle("~/Content/jquery").Include("~/Content/themes/base/jquery*"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap*"));
            

            BundleTable.EnableOptimizations = false;
        }
    }
}