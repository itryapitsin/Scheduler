function BaseController($scope, $q) {

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
}