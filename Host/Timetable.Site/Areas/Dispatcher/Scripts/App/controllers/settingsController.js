function settingsController($scope, $modal, $controller) {
    $controller('BaseController', { $scope: $scope });

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

    $scope.showCreateUserDialog = function () {
        var modalPromise = $modal({
            template: 'editusermodal.html',
            persist: true,
            show: false,
            backdrop: 'static',
            scope: $scope
        });

        this.showDialog(modalPromise);
    };

    hub.client.refreshLog = function (message, isCompleted) {
        $scope.$apply(function () {
            $scope.logs.push("[" + moment(new Date()).format("DD.MM.YYYY hh:mm") + "]: " + message);
            $scope.isSyncStarted = !isCompleted;
        });
    };

    $.connection.hub.start();
}

function CreateEditUserModalController($scope) {
    $scope.ok = function () {
        //this.hideDialog();
        
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}
