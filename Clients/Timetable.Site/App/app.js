

var app = angular.module('scheduler', ['ui.select2', 'ngResource', '$strap.directives', 'ngCookies', 'LocalStorageModule', 'ngProgress']);

app.config(function ($routeProvider, $locationProvider, $httpProvider) {

    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    var schedulerRoute = {
        templateUrl: prefix + 'scheduler',
        controller: schedulerController,
        resolve: schedulerController.prototype.resolve
    };

    $routeProvider
        .when('/scheduler', schedulerRoute)
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

app.run(function ($rootScope, $http) {
    var prefix = window.location.pathname;

    if (prefix[prefix.length - 1] != "/")
        prefix += "/";

    $rootScope.prefix = prefix;
    $http.prefix = prefix;
});
