﻿var app = angular.module('scheduler', ['ui.utils', 'ui.select2', 'ngResource', '$strap.directives', 'ngCookies', 'LocalStorageModule', 'ngRoute', 'ngDragDrop']);

app.config(function ($routeProvider) {

    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    var schedulerRoute = {
        templateUrl: prefix + 'scheduler',
        controller: SchedulerController,
    };

    var lecturerScheduleRoute = {
        templateUrl: prefix + 'lecturerschedule',
        controller: LecturerScheduleController,
    };

    var auditoriumScheduleRoute = {
        templateUrl: prefix + 'auditoriumschedule',
        controller: AuditoriumScheduleController,
    };

    var auditoriumScheduleGeneralRoute = {
        templateUrl: prefix + 'auditoriumschedule/general',
        controller: AuditoriumScheduleGeneralController,
    };

    var settingsRoute = {
        templateUrl: prefix + 'settings',
        controller: SettingsController,
    };

    $routeProvider
        .when('/scheduler', schedulerRoute)
        .when('/lecturerschedule', lecturerScheduleRoute)
        .when('/auditoriumschedule', auditoriumScheduleRoute)
        .when('/auditoriumschedule/general', auditoriumScheduleGeneralRoute)
        .when('/settings', settingsRoute)
        .otherwise({ redirectTo: '/scheduler' });
});

app.run(function ($rootScope, $http, $templateCache, $timeout) {
    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    $rootScope.prefix = prefix;
    $http.prefix = prefix;
    $rootScope.loading = true;

    $rootScope.$on('$routeChangeStart', function (s, e, q) {
        $rootScope.loading = true;
        if (e.$$route && $templateCache.get(e.$$route.templateUrl))
            $templateCache.remove(e.$$route.templateUrl);
    });

    $rootScope.$on('$viewContentLoaded', function (s, e, q) {
        $timeout(function () {
            $rootScope.loading = false;
        }, 500);
    });
});
