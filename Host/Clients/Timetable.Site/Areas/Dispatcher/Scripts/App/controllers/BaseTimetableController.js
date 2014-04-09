function BaseTimetableController($scope, $controller) {
    $controller('BaseController', { $scope: $scope });
    
    $scope.moment = moment;
    $scope.days = [1, 2, 3, 4, 5, 6, 7];
    
    $scope.findScheduleTickets = function (pair, dayOfWeek) {
        var result = $.Enumerable.From(this.schedules)
            .Where(function (x) {
                return x.pair == pair && x.dayOfWeek == dayOfWeek;
            })
            .ToArray();

        return result;
    };

   
    $scope.hasFullScheduleTicket = function (pair, dayOfWeek) {
        var result = $.Enumerable.From(this.schedules)
            .Where(function (x) {
                return x.pair == pair && x.dayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'Л';
        return false;
    };

    $scope.hasEvenScheduleTicket = function (pair, dayOfWeek) {
        var result = $.Enumerable.From(this.schedules)
            .Where(function (x) {
                return x.pair == pair && x.dayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].weekTypeName == 'Ч';

        return result.length > 1;
    };

    $scope.hasOddScheduleTicket = function (pair, dayOfWeek) {
        var result = $.Enumerable.From(this.schedules)
            .Where(function (x) {
                return x.pair == pair && x.dayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].weekTypeName == 'З';

        return result.length > 1;
    };

    $scope.hasScheduleTicket = function (pair, dayOfWeek) {
        var result = $.Enumerable.From(this.schedules)
            .Where(function (x) {
                return x.pair == pair && x.dayOfWeek == dayOfWeek;
            })
            .Count();

        return result > 0;
    };

    $scope.isFullScheduleTicket = function (schedule) {
        return schedule.weekTypeName == 'Л';
    };

    $scope.isEvenScheduleTicket = function (schedule) {
        return schedule.weekTypeName == 'Ч';
    };

    $scope.isOddScheduleTicket = function (schedule) {
        return schedule.weekTypeName == 'З';
    };
}