app.factory('timetableService', ['$http', '$q', '$resource', function () {
    var service = {};

    service.findScheduleTickets = function (time, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        return result;
    };

    service.hasFullScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'Л';
        return false;
    };

    service.hasEvenScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'Ч';

        return result.length > 1;
    };

    service.hasOddScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'З';

        return result.length > 1;
    };

    service.hasScheduleTicket = function (pair, dayOfWeek) {
        var result = $.Enumerable.From(this.$parent.schedules)
            .Where(function (x) {
                return x.TimeId == pair && x.DayOfWeek == dayOfWeek;
            })
            .Count();

        return result > 0;
    };

    service.isFullScheduleTicket = function (schedule) {
        return schedule.WeekTypeName == 'Л';
    };

    service.isEvenScheduleTicket = function (schedule) {
        return schedule.WeekTypeName == 'Ч';
    };

    service.isOddScheduleTicket = function (schedule) {
        return schedule.WeekTypeName == 'З';
    };

    return service;
}]);