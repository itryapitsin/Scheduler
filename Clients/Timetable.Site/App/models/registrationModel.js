app.factory('registrationModel', function ($http, $cookies, auth, $location) {

    function model() {
        this.submit = function () {
            $http.post(
                $http.prefix + "api/registration",
                {
                    login: this.login,
                    password: this.password,
                    confirmPassword: this.confirmPassword
                })
                .success(function (result) {
                    auth.user({ username: result.Identity.Name, role: auth.userRoles.public });
                    auth.setToken(result.Token);

                    $location.path('/');
                });
        };
    }

    return new model();
});