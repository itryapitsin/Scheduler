baseController = {};

baseController.resolve = {
    delay: function ($q, $timeout, ngProgress) {
        var delay = $q.defer();
        ngProgress.start();

        $timeout(function () {
            delay.resolve();
            ngProgress.complete();
        }, 1000);

        return delay.promise;
    }
};