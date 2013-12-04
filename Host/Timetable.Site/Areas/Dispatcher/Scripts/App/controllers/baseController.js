baseController = {};

baseController.resolve = {
    delay: function ($q, $timeout) {
        var delay = $q.defer();
        //ngLoading.start();

        //$timeout(function () {
            delay.resolve();
        //    //ngLoading.complete();
        //}, 1000);

        //$q.when(delay.promise)
        //    .then(function() { alert("test"); });

        return delay.promise;
    }
};