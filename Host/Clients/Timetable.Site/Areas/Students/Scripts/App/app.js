var app = angular.module('scheduler', ['ui.select2', 'ngResource', 'ngRoute', '$strap.directives', 'ngCookies', 'LocalStorageModule']);

app.config(function ($routeProvider) {

    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    var threadScheduleRoute = {
        templateUrl: prefix + 'threadschedule',
        controller: ThreadScheduleController,
    };

    var lecturerScheduleRoute = {
        templateUrl: prefix + 'lecturerschedule',
        controller: LecturerScheduleController,
    };
    
    var auditoriumScheduleRoute = {
        templateUrl: prefix + 'auditoriumschedule',
        controller: AuditoriumScheduleController,
    };

    $routeProvider
        .when('/threadschedule', threadScheduleRoute)
        .when('/lecturerschedule', lecturerScheduleRoute)
        .when('/auditoriumschedule', auditoriumScheduleRoute)
        .otherwise({ redirectTo: '/threadschedule' });
});

app.run(function ($rootScope, $http, $templateCache, $timeout) {
    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    $rootScope.prefix = prefix;
    $http.prefix = prefix;
    
    $rootScope.loading = true;
    
    $http.defaults.transformRequest.push(function (data) {
        $rootScope.loading = true;
        return data;
    });

    $http.defaults.transformResponse.push(function (data) {
        $timeout(function () {
            $rootScope.loading = false;
        }, 500);
        return data;
    });

    $rootScope.$on('$routeChangeStart', function () {
        $rootScope.loading = true;
    });
    
    $rootScope.$on('$routeChangeStart', function () {
        $rootScope.loading = true;
    });

    $rootScope.$on('$viewContentLoaded', function () {
        $templateCache.removeAll();

        $timeout(function () {
            $rootScope.loading = false;
        }, 500);
    });
});

