function settingsController($scope, $http) {
    $scope.refreshDataClick = function() {

    };

    $scope.logs = [];
    $scope.moment = moment;
    
    var hub = $.connection.dispatcherHub;
    
    $scope.refreshLogClick = function () {
        $http.get($http.prefix + 'Settings/RefreshLog');
    };
    
    hub.client.refreshLog = function (message) {
        $scope.logs.push("[" + moment(new Date()).format("DD.MM.YYYY hh:mm") + "]: " + message);
    };

    $.connection.hub.start();
}

settingsController.prototype = baseController;