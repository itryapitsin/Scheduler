function BaseTimetableController($scope, $controller) {
    $controller('BaseController', { $scope: $scope });
    
    $scope.moment = moment;
    $scope.days = [1, 2, 3, 4, 5, 6, 7];
    
    $scope.findScheduleTickets = function (time, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        return result;
    };

    $scope.hasFullScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'Л';
        return false;
    };

    $scope.hasEvenScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'Ч';

        return result.length > 1;
    };

    $scope.hasOddScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'З';

        return result.length > 1;
    };

    $scope.hasScheduleTicket = function (pair, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == pair && x.DayOfWeek == dayOfWeek;
            })
            .Count();

        return result > 0;
    };

    $scope.isFullScheduleTicket = function (schedule) {
        return schedule.WeekTypeName == 'Л';
    };

    $scope.isEvenScheduleTicket = function (schedule) {
        return schedule.WeekTypeName == 'Ч';
    };

    $scope.isOddScheduleTicket = function (schedule) {
        return schedule.WeekTypeName == 'З';
    };
}