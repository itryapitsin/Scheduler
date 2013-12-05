function settingsController($scope, $controller) {
    $controller('BaseController', { $scope: $scope });

    $scope.refreshDataClick = function() {
    };
}

function UserSettingsController($scope, $http, $rootScope) {
    $scope.login = pageModel.login;
    $scope.firstname = pageModel.firstname;
    $scope.middlename = pageModel.middlename;
    $scope.lastname = pageModel.lastname;
    
    $scope.save = function () {
        var params = {
            firstname: $scope.firstname,
            middlename: $scope.middlename,
            lastname: $scope.lastname,
        };

        $http
            .post($http.prefix + 'Settings/Save',  params)
            .success(function (response) {
                if (response.ok) {
                    
                    $rootScope.$broadcast('settingsUpdated', response.userName);
                }

                if (response.message)
                    $scope.message = response.message;
                else
                    $scope.message = 'Ошибка запроса';
            });
    };
}

function UsersController($scope, $modal) {
    $scope.users = pageModel.users;

    $scope.$on('userCreated', function(e, user) {
        $scope.users.push(user);
    });
    
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

function CreateEditUserModalController($scope, $http, $controller, $rootScope) {
    $controller('BaseController', { $scope: $scope });

    $scope.ok = function () {
        var params = {
            firstname: $scope.firstname,
            middlename: $scope.middlename,
            lastname: $scope.lastname,
            login: $scope.login,
            password: $scope.password,
            confirmPassword: $scope.confirmPassword
        };

        $http
            .post($http.prefix + 'Settings/CreateEditUser', params)
            .success(function(response) {
                if (response.ok) {
                    $scope.hideDialog();
                    $rootScope.$broadcast('userCreated', {
                        firstname: $scope.firstname,
                        middlename: $scope.middlename,
                        lastname: $scope.lastname,
                        login: $scope.login,
                    });
                }

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