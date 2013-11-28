var app = angular.module('scheduler', ['ui.select2', 'ngResource', '$strap.directives', 'ngCookies', 'LocalStorageModule', 'ngProgress', 'ngDragDrop']);

app.config(function ($routeProvider) {

    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    var schedulerRoute = {
        templateUrl: prefix + 'scheduler',
        controller: schedulerController,
        resolve: schedulerController.prototype.resolve
    };

    var lecturerScheduleRoute = {
        templateUrl: prefix + 'lecturerschedule',
        controller: lecturerScheduleController,
        resolve: lecturerScheduleController.prototype.resolve
    };
    
    var auditoriumScheduleRoute = {
        templateUrl: prefix + 'auditoriumschedule',
        controller: auditoriumScheduleController,
        resolve: auditoriumScheduleController.prototype.resolve
    };
    
    var auditoriumScheduleGeneralRoute = {
        templateUrl: prefix + 'auditoriumschedule/general',
        controller: auditoriumScheduleGeneralController,
        resolve: auditoriumScheduleGeneralController.prototype.resolve
    };
    
    var settingsRoute = {
        templateUrl: prefix + 'settings',
        controller: settingsController,
        resolve: settingsController.prototype.resolve
    };

    $routeProvider
        .when('/scheduler', schedulerRoute)
        .when('/lecturerschedule', lecturerScheduleRoute)
        .when('/auditoriumschedule', auditoriumScheduleRoute)
        .when('/auditoriumschedule/general', auditoriumScheduleGeneralRoute)
        .when('/settings', settingsRoute)
        .otherwise({ redirectTo: '/scheduler' });
});

app.run(function ($rootScope, $http) {
    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    $rootScope.prefix = prefix;
    $http.prefix = prefix;
});

