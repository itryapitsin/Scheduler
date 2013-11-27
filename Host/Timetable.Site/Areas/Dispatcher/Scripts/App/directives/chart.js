app.directive('chart', ['$filter', function ($filter) {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function(scope, elem, attrs, tr) {
            var ctx = elem.get(0).getContext("2d");

            var pairs = $.map(scope[attrs.ngModel], function (item) {
                return {
                    date: $filter(attrs.filter)(item[attrs.label], 'dd.MM.yyyy'),
                    value: item[attrs.value]
                };
            });

            var grouping = $.Enumerable.From(pairs)
                .OrderBy("$.date")
                .GroupBy("$.date")
                .ToArray();
            
            var values = $.Enumerable.From(grouping)
                .Select(function (item) { return $.Enumerable.From(item.source).Sum("$.value"); })
                .ToArray();
            
            var labels = $.Enumerable.From(grouping)
                .Select(function(item) { return item.Key(); })
                .ToArray();

            var data = {
                labels: labels,
                datasets: [
                    {
                        fillColor: "rgba(151,187,205,0.5)",
                        strokeColor: "rgba(151,187,205,1)",
                        pointColor: "rgba(151,187,205,1)",
                        pointStrokeColor: "#fff",
                        data: values
                    }
                ]
            };

            var chart = new Chart(ctx);

            chart[attrs.type](data);
        }
    };
}]);
