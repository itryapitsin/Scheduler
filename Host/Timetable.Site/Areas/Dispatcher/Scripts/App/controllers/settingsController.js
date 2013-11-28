function settingsController($scope, $http) {
    $scope.refreshDataClick = function() {

    };

    $scope.logs = [];
    $scope.moment = moment;
    $scope.isSyncStarted = false;
    var hub = $.connection.dispatcherHub;
    
    $scope.refreshLogClick = function () {
        $scope.logs = [];
        $scope.isSyncStarted = true;
        hub.server.refreshData();
    };
    
    hub.client.refreshLog = function (message, isCompleted) {
        $scope.$apply(function() {
            $scope.logs.push("[" + moment(new Date()).format("DD.MM.YYYY hh:mm") + "]: " + message);
            $scope.isSyncStarted = !isCompleted;
        });
    };

    $.connection.hub.start();
}

settingsController.prototype = baseController;