function BaseController($scope, $q, $timeout) {

    $scope.showDialog = function(modalPromise) {
        self.promise = modalPromise;

        $q.when(self.promise).then(function(modalEl) {
            modalEl.modal('show');
            modalEl.removeClass("hide");
        });
    };

    $scope.hideDialog = function () {
        $q.when(self.promise).then(function(modalEl) {
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