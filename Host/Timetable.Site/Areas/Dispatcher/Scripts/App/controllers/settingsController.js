function settingsController($scope, $controller) {
    $controller('BaseController', { $scope: $scope });

    $scope.refreshDataClick = function() {
    };
}

function UserSettingsController($scope, $http, $rootScope, $modal) {
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

function UsersController($scope, $modal, $http) {
    $scope.users = pageModel.users;

    $scope.$on('userCreated', function(e, user) {
        $scope.users.push(user);
    });

    $scope.delete = function (user) {
        $scope.confirm = {
            content: "Вы действительно хотите удалить пользователя '{0}' ?".replace('{0}', user.login),
            ok: function () {
                $http
                    .post()
                    .success(function(response) {

                    });

                $scope.hideDialog();
            },
            cancel: function() {
                $scope.hideDialog();
            }
        };
        var modalPromise = $modal({
            template: 'confirmation.html',
            persist: true,
            show: false,
            backdrop: 'static',
            scope: $scope
        });

        this.showDialog(modalPromise);
    };
    
    $scope.edit = function (user) {
        $scope.login = user.login;
        $scope.firstname = user.firstname;
        $scope.middlename = user.middlename;
        $scope.lastname = user.lastname;
        $scope.isEdit = true;

        var modalPromise = $modal({
            template: 'editusermodal.html',
            persist: true,
            show: false,
            backdrop: 'static',
            scope: $scope
        });

        this.showDialog(modalPromise);
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