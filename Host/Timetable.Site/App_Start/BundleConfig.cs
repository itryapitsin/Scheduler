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
                "~/Scripts/angular-select2.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-scenario.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-strap.js",
                "~/Scripts/angular-touch.js",
                "~/Scripts/angular-bootstrap.js",
                "~/Scripts/angular-bootstrap-prettify.js",
                "~/Scripts/angular-localstorage.js",
                "~/Scripts/angular-progress.js",
                "~/Scripts/angular-dragndrop.js",
                "~/Scripts/angular-sortable.js",
                "~/Scripts/ui-bootstrap-{version}.js",
                "~/Scripts/ui-bootstrap-tpls-{version}.js",
                "~/Scripts/i18n/angular-locale_ru-ru.js"));

            bundles.Add(new ScriptBundle("~/Scripts/select2").Include("~/Scripts/select2.js"));

            bundles.Add(new ScriptBundle("~/Dispatcher/App").Include(
                "~/Scripts/underscore.js",
                "~/Areas/Dispatcher/Scripts/App/app.js",
                "~/Areas/Dispatcher/Scripts/App/Directives/daterangepicker.js",
                "~/Areas/Dispatcher/Scripts/App/Directives/scheduleCard.js",
                "~/Areas/Dispatcher/Scripts/App/Services/facultyService.js",
                "~/Areas/Dispatcher/Scripts/App/Services/specialityService.js",
                "~/Areas/Dispatcher/Scripts/App/Services/groupService.js",
                "~/Areas/Dispatcher/Scripts/App/Models/refreshDataLocker.js",
                "~/Areas/Dispatcher/Scripts/App/Models/settingsModel.js",
                "~/Areas/Dispatcher/Scripts/App/Models/threadModel.js",
                "~/Areas/Dispatcher/Scripts/App/Models/scheduleCreatorModel.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/baseController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/navbarController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/settingsController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/schedulerController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/lecturerScheduleController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/auditoriumScheduleController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/auditoriumScheduleGeneralController.js"));

            bundles.Add(new ScriptBundle("~/Students/App").Include(
                "~/Areas/Students/Scripts/App/app.js",
                "~/Areas/Students/Scripts/App/Directives/daterangepicker.js",
                "~/Areas/Students/Scripts/App/Directives/scheduleCard.js",
                "~/Areas/Students/Scripts/App/Services/timetableService.js",
                "~/Areas/Students/Scripts/App/Controllers/baseController.js",
                "~/Areas/Students/Scripts/App/Controllers/navbarController.js",
                "~/Areas/Students/Scripts/App/Controllers/threadScheduleController.js",
                "~/Areas/Students/Scripts/App/Controllers/lecturerScheduleController.js",
                "~/Areas/Students/Scripts/App/Controllers/auditoriumScheduleController.js"));

            bundles.Add(new StyleBundle("~/Content/Site").Include("~/Content/Site.css"));
            bundles.Add(new StyleBundle("~/Content/jquery").Include("~/Content/themes/base/jquery*"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap*"));
            bundles.Add(new StyleBundle("~/Content/select2").Include("~/Content/css/select2.css"));
            bundles.Add(new StyleBundle("~/Content/signin").Include(
                "~/Content/style.css",
                "~/Content/demo.css",
                "~/Content/font-awesome.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}