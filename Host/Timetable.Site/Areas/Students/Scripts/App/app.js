var app = angular.module('scheduler', ['ui.select2', 'ngResource', '$strap.directives', 'ngCookies', 'LocalStorageModule', 'ngProgress']);

app.config(function ($routeProvider) {

    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    var threadScheduleRoute = {
        templateUrl: prefix + 'threadschedule',
        controller: threadScheduleController,
        resolve: threadScheduleController.prototype.resolve
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

    $routeProvider
        .when('/threadschedule', threadScheduleRoute)
        .when('/lecturerschedule', lecturerScheduleRoute)
        .when('/auditoriumschedule', auditoriumScheduleRoute)
        .otherwise({ redirectTo: '/threadschedule' });
});

app.run(function ($rootScope, $http) {
    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    $rootScope.prefix = prefix;
    $http.prefix = prefix;
});

