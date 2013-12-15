app.factory('loginModel', function ($http, $cookies, auth, $location, $window) {
    function model() {
        var self = this;

        this.submit = function() {
            $http.post(
                $http.prefix + "api/auth",
                {
                    login: this.login,
                    password: this.password
                })
                .success(function (result) {
                    auth.user({ username: result.Identity.Name, role: auth.userRoles.public });
                    auth.setToken(result.Token);

                    delete self.error;

                    $location.path('/');
                })
                .error(function (data, status, headers, config) {
                    self.error = "Авторизация неудалась";
                });
        };
    }

    return new model();
});