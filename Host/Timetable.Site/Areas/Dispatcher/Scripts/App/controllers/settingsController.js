function settingsController($scope, $controller) {
    $controller('BaseController', { $scope: $scope });

    $scope.refreshDataClick = function() {
    };
}

function UsersController($scope, $modal) {
    $scope.users = pageModel.users;
    
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
}

function CreateEditUserModalController($scope, $http) {
    $scope.ok = function () {
        $http
            .post()
            .success(function(response) {
                if (response.ok)
                    this.hideDialog();
                
                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };

    $scope.cancel = function () {
        this.hideDialog();
    };
}

function DataSyncController($scope) {
    var hub = $.connection.dispatcherHub;

    $scope.logs = [];
    $scope.moment = moment;
    $scope.isSyncStarted = false;

    $scope.syncDataClick = function () {
        $scope.logs = [];
        $scope.isSyncStarted = true;
        hub.server.refreshData();
    };
    
    hub.client.refreshLog = function (message, isCompleted) {
        $scope.$apply(function () {
            $scope.logs.push("[" + moment(new Date()).format("DD.MM.YYYY hh:mm") + "]: " + message);
            $scope.isSyncStarted = !isCompleted;
        });
    };
    
    $.connection.hub.start();
}