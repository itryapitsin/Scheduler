app.directive('daterangepicker', [function () {
    return {
        restrict: 'A',
        link: function(scope, elem, attrs, tr) {
            $(elem).daterangepicker({
                format: 'DD.MM.YYYY'
            });
        }
    };
}]);
