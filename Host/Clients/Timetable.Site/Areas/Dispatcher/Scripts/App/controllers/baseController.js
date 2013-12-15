function BaseController($scope, $q, $timeout, $window, $modal) {

    $scope.showDialog = function (templateUrl) {
        var self = this;
        var modalPromise = $modal({
            template: templateUrl,
            persist: true,
            show: false,
            backdrop: 'static',
            scope: self
        });

        $window.promise = modalPromise;

        $q.when($window.promise).then(function (modalEl) {
            modalEl.modal('show');
            modalEl.removeClass("hide");
        });
    };

    $scope.hideDialog = function () {
        $q.when($window.promise).then(function (modalEl) {
            modalEl.modal('hide');
            modalEl.addClass("hide");
        });
    };

    $scope.clearAfterTimeout = function (prop, timeout) {
        var scope = this;

        $timeout(function() {
            delete scope[prop];
        }, timeout ? timeout : 5000);
    };
}