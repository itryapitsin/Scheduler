﻿function auditoriumScheduleController($scope, $http) {
    $scope.moment = moment;
    $scope.pageModel = pageModel;
    $scope.building = pageModel.BuildingId;
    $scope.auditorium = pageModel.AuditoriumId;
    $scope.auditoriums = pageModel.Auditoriums;
    $scope.times = pageModel.Times;
    $scope.studyYear = pageModel.StudyYearId;
    $scope.semester = pageModel.Semester;
    $scope.schedules = pageModel.Schedules;

    $scope.loadAuditoriums = function (building) {
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetAuditoriums', { params: { buildingId: building } })
            .success(function (response) {
                $scope.auditoriums = response.Auditoriums;
                $scope.times = response.Times;
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
                return x.TimeId == timeId && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        return result;
    };
    
    $scope.findScheduleTickets = function (time, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        return result;
    };

    $scope.hasFullScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'Л';
        return false;
    };

    $scope.hasEvenScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
            })
            .ToArray();

        if (result.length == 1)
            return result[0].WeekTypeName == 'Ч';

        return result.length > 1;
    };

    $scope.hasOddScheduleTicket = function (time, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function (x) {
                return x.TimeId == time.Id && x.DayOfWeek == dayOfWeek;
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

auditoriumScheduleController.prototype = baseController;