var userRoles = {
    public: 1,
    user: 2
};

var accessLevels = {
    public: userRoles.public | userRoles.user,
    user: userRoles.user
};

app.factory('auth', function ($cookieStore, $cookies) {
    var tokenKey = '.ASPXAUTH';
    var userKey = "user";

    return {
        accessLevels: accessLevels,
        userRoles: userRoles,
        
        logout: function () {
            //$cookieStore.remove(userKey);
            //$cookieStore.remove(tokenKey);
            delete $cookies[tokenKey];
            delete $cookies[userKey];
        },

        isLoggedIn: function (user) {
            if (!user)
                user = this.user;
            return user.role === userRoles.user || user.role === userRoles.admin;
        },
        
        setToken: function (token) {
            $cookies[tokenKey] = token;
        },
        
        user: function (user) {
            if (user)
                $cookies[userKey] = JSON.stringify(user);

            return $cookieStore.get(userKey) || { username: '', role: userRoles.public };
        }
    };
});