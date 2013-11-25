function auditoriumScheduleGeneralController($scope, $http) {
    $scope.moment = moment;
    $scope.pageModel = pageModel;
    $scope.building = pageModel.BuildingId;
    $scope.schedules = pageModel.Schedules;
    $scope.auditoriumType = pageModel.AuditoriumTypeId;
    $scope.auditoriums = pageModel.Auditoriums;
    $scope.times = pageModel.Times;
    $scope.studyYear = pageModel.StudyYearId;
    $scope.semester = pageModel.Semester;
    
    $scope.buildingChanged = function () {
        loadAuditoriumsAndSchedules();
    };

    $scope.auditoriumTypeChanged = function () {
        loadAuditoriumsAndSchedules();
    };

    function loadAuditoriumsAndSchedules() {
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetAuditoriumsAndSchedules',
                {
                    params: {
                        buildingId: $scope.building,
                        auditoriumTypeId: $scope.auditoriumType,
                        studyYearId: $scope.studyYear,
                        semester: $scope.semester
                    }
                })
            .success(function (response) {
                $scope.auditoriums = response.Auditoriums;
                $scope.schedules = response.Schedules;
                $scope.times = response.Times;
            });
    }

    $scope.loadAuditoriumsSchedule = function () {
        $http
            .get($http.prefix + 'AuditoriumSchedule/LoadAuditoriumsSchedule',
                {
                    params: {
                        buildingId: $scope.building,
                        auditoriumTypeId: $scope.auditoriumType,
                        studyYearId: $scope.studyYear,
                        semester: $scope.semester
                    }
                })
            .success(function (response) {
                $scope.schedules = response;
            });
    };

    $scope.findScheduleTicket = function (timeId, dayOfWeek) {
        var result = $.Enumerable.From($scope.schedules)
            .Where(function (x) {
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

    $scope.isBusy = function (auditorium, day, time) {
        var hasSchedule = $.Enumerable.From($scope.schedules)
            .Where(function(item) {
                return item.AuditoriumNumber == auditorium.Number && item.DayOfWeek == day && item.TimeId == time.Id;
            })
            .ToArray();

        return hasSchedule.length > 0;
    };

    //var hub = $.connection.dispatcherHub;
    //hub.client.broadcastMessage = function (name, message) {
    //    alert(name + ": " + message);
    //};

    //$.connection.hub.start().done(function () {
    //        // Call the Send method on the hub. 
    //        hub.server.send("Tester", "Hello world!");
    //});
}

auditoriumScheduleGeneralController.prototype = baseController;