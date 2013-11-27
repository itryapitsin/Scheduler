app.factory('specialityService', ['$http', '$q', '$resource', function ($http, $q, $resource) {
    
    var factory = {
        get: function (branchId) {
            var deferred = $q.defer();
            var transactions = $resource(
                $http.prefix + 'speciality/GetForBranch',
                {},
                {
                    get: { method: "GET", isArray: false },
                });

            transactions.get(
                { branchId: branchId },
                function (resp) {
                    deferred.resolve(resp);
                });
            
            return deferred.promise;
        }
    };
    return factory;
}]);