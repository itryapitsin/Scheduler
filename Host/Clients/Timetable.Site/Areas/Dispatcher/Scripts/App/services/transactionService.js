app.factory('financesService', ['$http', '$q', '$resource', 'promiseTracker', function ($http, $q, $resource, promiseTracker) {
    
    var factory = {
        get: function (params) {
            var deferred = $q.defer();
            var transactions = $resource(
                $http.prefix + 'api/finances/:type/:year/:month',
                {},
                {
                    get: { method: "GET", isArray: true },
                });

            var request = transactions.get({
                    type: params.type,
                    year: params.year,
                    month: params.month,
                },
                function (resp) {
                    deferred.resolve(resp);
                });
            
            return deferred.promise;
        },
        
        save: function(params) {
            var deferred = $q.defer();
            var url;

            if(params.id)
                url = $http.prefix + 'api/finances/update';
            else
                url = $http.prefix + 'api/finances/create';

            var transactions = $resource(
                url,
                {},
                { save: { method: "POST" } });

            transactions.save(
                params,
                function (resp) {
                    deferred.resolve(resp);
                });
            return deferred.promise;
        },
        
        delete: function (id) {
            var deferred = $q.defer();
            
            var transactions = $resource(
                $http.prefix + 'api/finances/delete',
                {},
                {
                    delete: { method: "POST" }
                });

            transactions.delete({ id: id },
                function (resp) {
                    deferred.resolve(resp);
                });
            return deferred.promise;
        }
    };
    return factory;
}]);