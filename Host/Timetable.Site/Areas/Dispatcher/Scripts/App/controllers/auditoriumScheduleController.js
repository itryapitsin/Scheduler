function auditoriumScheduleController($scope, $http) {
    $scope.moment = moment;
    $scope.pageModel = pageModel;
    $scope.building = pageModel.buildingId;
    $scope.auditorium = pageModel.auditoriumId;
    $scope.auditoriums = pageModel.auditoriums;
    $scope.times = pageModel.times;
    $scope.studyYear = pageModel.studyYearId;
    $scope.semester = pageModel.semester;
    $scope.schedules = pageModel.schedules;
    $scope.days = [1, 2, 3, 4, 5, 6, 7];

    $scope.getReportForAuditorium = function () {

        document.location.href = '/Report/GetReportForAuditorium?auditoriumId={0}&semester={1}&studyYearId={2}&title="sometitle"'
            .replace('{0}', $scope.auditorium)
            .replace('{1}', $scope.semester)
            .replace('{2}', $scope.studyYear);
    };

    $scope.loadAuditoriums = function (building) {
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetAuditoriums', { params: { buildingId: building } })
            .success(function (response) {
                $scope.auditoriums = response.auditoriums;
                $scope.times = response.times;
            });
    };
    
    $scope.loadAuditoriumSchedule = function () {
        $http
            .get($http.prefix + 'AuditoriumSchedule/LoadAuditoriumSchedule',
                {
                    params: {
                        auditoriumId: $scope.auditorium,
                        studyYearId: $scope.studyYear,
                        semester: $scope.semester
                    }
                })
            .success(function (response) {
                $scope.schedules = response;
            });
    };
    
    $scope.findScheduleTicket = function(timeId, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function(x) {
                return x.timeId == timeId && x.dayOfWeek == dayOfWeek;
            })
            .ToArray();

        return result;
    };
    
    $scope.findScheduleTickets = function (time, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function (x) {
                return x.timeId == time.Id && x.dayOfWeek == dayOfWeek;
            })
            .ToArray();

        return result;
    };

    $scope.hasFullScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function (x) {
                return x.timeId == time.Id && x.dayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'Л';
        return false;
    };

    $scope.hasEvenScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function (x) {
                return x.timeId == time.Id && x.dayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'Ч';

        return result.length > 1;
    };

    $scope.hasOddScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function (x) {
                return x.timeId == time.Id && x.dayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'З';

        return result.length > 1;
    };

    $scope.hasScheduleTicket = function (pair, dayOfWeek) {
        var result = self.findScheduleTicket(pair, dayOfWeek);
        return result.length > 0;
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
