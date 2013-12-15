app.factory('groupService', ['$http', '$q', '$resource', function ($http, $q, $resource) {
    
    var factory = {
        get: function (params) {
            var deferred = $q.defer();
            var transactions = $resource(
                $http.prefix + 'group/get',
                {},
                {
                    get: { method: "GET", isArray: false },
                });

            transactions.get(
                params,
                function (resp) {
                    deferred.resolve(resp);
                });
            
            return deferred.promise;
        }
    };
    return factory;
}]);