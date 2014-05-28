using System.Web.Optimization;
using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;

namespace Timetable.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var nullBuilder = new NullBuilder();
            var cssTransformer = new CssTransformer();
            var jsTransformer = new JsTransformer();
            var nullOrderer = new NullOrderer();

            var jqueryBundle = new Bundle("~/Scripts/jquery");
            jqueryBundle.Include("~/Scripts/jquery*");
            jqueryBundle.Builder = nullBuilder;
            jqueryBundle.Transforms.Add(jsTransformer);
            jqueryBundle.Orderer = nullOrderer;

            var bootstrapBundle = new Bundle("~/Scripts/bootstrap");
            bootstrapBundle.Include(
                "~/Scripts/bootstrap*",
                "~/Scripts/typeahead.js");
            bootstrapBundle.Builder = nullBuilder;
            bootstrapBundle.Transforms.Add(jsTransformer);
            bootstrapBundle.Orderer = nullOrderer;

            var momentBundle = new Bundle("~/Scripts/moment");
            momentBundle.Include("~/Scripts/moment*");
            momentBundle.Builder = nullBuilder;
            momentBundle.Transforms.Add(jsTransformer);
            momentBundle.Orderer = nullOrderer;

            var angularBundle = new Bundle("~/Scripts/angular");
            angularBundle.Include(
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
                "~/Scripts/angular-loading.js",
                "~/Scripts/angular-dragndrop.js",
                "~/Scripts/angular-sortable.js",
                "~/Scripts/ui-utils.js",
                "~/Scripts/ui-bootstrap-{version}.js",
                "~/Scripts/ui-bootstrap-tpls-{version}.js",
                "~/Scripts/i18n/angular-locale_ru-ru.js");
            angularBundle.Builder = nullBuilder;
            angularBundle.Transforms.Add(jsTransformer);
            angularBundle.Orderer = nullOrderer;

            var select2Bundle = new Bundle("~/Scripts/select2");
            select2Bundle.Include("~/Scripts/select2.js");
            select2Bundle.Builder = nullBuilder;
            select2Bundle.Transforms.Add(jsTransformer);
            select2Bundle.Orderer = nullOrderer;

            var uiBootstrapBundle = new Bundle("~/Scripts/ui-bootstrap-0.10.0");
            uiBootstrapBundle.Include("~/Scripts/ui-bootstrap-0.10.0.js");
            uiBootstrapBundle.Builder = nullBuilder;
            uiBootstrapBundle.Transforms.Add(jsTransformer);
            uiBootstrapBundle.Orderer = nullOrderer;

            var dispatcherApp = new Bundle("~/Dispatcher/App");
            dispatcherApp.Include(
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
                "~/Scripts/Site/BaseController.js",
                "~/Scripts/Site/BaseTimetableController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/navbarController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/settingsController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/schedulerController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/lecturerScheduleController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/sessionScheduleController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/auditoriumScheduleController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/auditoriumScheduleOrderController.js",
                "~/Areas/Dispatcher/Scripts/App/Controllers/auditoriumScheduleGeneralController.js");
            dispatcherApp.Builder = nullBuilder;
            dispatcherApp.Transforms.Add(jsTransformer);
            dispatcherApp.Orderer = nullOrderer;

            var studentApp = new Bundle("~/Students/App");
            studentApp.Include(
                "~/Areas/Students/Scripts/App/app.js",
                "~/Areas/Students/Scripts/App/Directives/daterangepicker.js",
                "~/Areas/Students/Scripts/App/Directives/scheduleCard.js",
                "~/Scripts/Site/BaseController.js",
                "~/Scripts/Site/BaseTimetableController.js",
                "~/Areas/Students/Scripts/App/Controllers/baseController.js",
                "~/Areas/Students/Scripts/App/Controllers/navbarController.js",
                "~/Areas/Students/Scripts/App/Controllers/threadScheduleController.js",
                "~/Areas/Students/Scripts/App/Controllers/lecturerScheduleController.js",
                "~/Areas/Students/Scripts/App/Controllers/auditoriumScheduleController.js");
            studentApp.Builder = nullBuilder;
            studentApp.Transforms.Add(jsTransformer);
            studentApp.Orderer = nullOrderer;

            bundles.Add(jqueryBundle);
            bundles.Add(bootstrapBundle);
            bundles.Add(momentBundle);
            bundles.Add(angularBundle);
            bundles.Add(select2Bundle);
            bundles.Add(uiBootstrapBundle);
            bundles.Add(dispatcherApp);
            bundles.Add(studentApp);

            var siteStyleBundle = new Bundle("~/Content/Site");
            siteStyleBundle.Include("~/Content/Site.less");
            siteStyleBundle.Builder = nullBuilder;
            siteStyleBundle.Transforms.Add(cssTransformer);
            siteStyleBundle.Orderer = nullOrderer;

            var bootstrapStyleBundle = new Bundle("~/Content/bootstrap");
            bootstrapStyleBundle.Include("~/Content/bootstrap/bootstrap.less");
            bootstrapStyleBundle.Builder = nullBuilder;
            bootstrapStyleBundle.Transforms.Add(cssTransformer);
            bootstrapStyleBundle.Orderer = nullOrderer;
            
            //var datepickerStyleBundle = new Bundle("~/Content/bootstrap");
            //datepickerStyleBundle.Include("~/Content/bootstrap/datepicker3.less");
            //datepickerStyleBundle.Builder = nullBuilder;
            //datepickerStyleBundle.Transforms.Add(cssTransformer);
            //datepickerStyleBundle.Orderer = nullOrderer;

            var select2StyleBundle = new Bundle("~/Content/select2");
            select2StyleBundle.Include("~/Content/css/select2.css");
            select2StyleBundle.Builder = nullBuilder;
            select2StyleBundle.Transforms.Add(cssTransformer);
            select2StyleBundle.Orderer = nullOrderer;

            var signinStyleBundle = new Bundle("~/Content/signin");
            signinStyleBundle.Include(
                "~/Content/style.css",
                "~/Content/demo.css",
                "~/Content/font-awesome.css");
            signinStyleBundle.Builder = nullBuilder;
            signinStyleBundle.Transforms.Add(cssTransformer);
            signinStyleBundle.Orderer = nullOrderer;

            bundles.Add(siteStyleBundle);
            bundles.Add(bootstrapStyleBundle);
            //bundles.Add(datepickerStyleBundle);
            bundles.Add(select2StyleBundle);
            bundles.Add(signinStyleBundle);
        }
    }
}