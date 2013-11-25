﻿var app = angular.module('scheduler', ['ui.select2', 'ngResource', '$strap.directives', 'ngCookies', 'LocalStorageModule', 'ngProgress', 'ngDragDrop']);

app.config(function ($routeProvider, $locationProvider, $httpProvider) {

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

    $routeProvider
        .when('/scheduler', schedulerRoute)
        .when('/lecturerschedule', lecturerScheduleRoute)
        .when('/auditoriumschedule', auditoriumScheduleRoute)
        .when('/auditoriumschedule/general', auditoriumScheduleGeneralRoute)
        .otherwise({ redirectTo: '/scheduler' });

    //var interceptor = ['$location', '$q', function ($location, $q) {
    //    function success(response) {
    //        return response;
    //    }

    //    function error(response) {

    //        if (response.status === 401) {
    //            $location.path('/login');
    //            return $q.reject(response);
    //        }
    //        else {
    //            return $q.reject(response);
    //        }
    //    }

    //    return function (promise) {
    //        return promise.then(success, error);
    //    };
    //}];

    //$httpProvider.responseInterceptors.push(interceptor);
});

app.filter("owner", function () {
    return _.memoize(function (collection) {
        return collection;
    });
});

app.run(function ($rootScope, $http) {
    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    $rootScope.prefix = prefix;
    $http.prefix = prefix;
});

