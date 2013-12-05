function lecturerScheduleController($scope, $http) {
    $scope.moment = moment;

    $scope.lecturer = pageModel.LecturerSearchString;

    $scope.schedules = [];
    $scope.times = [];
    $scope.pageModel = pageModel;
    $scope.studyYear = pageModel.StudyYearId;
    $scope.semester = pageModel.Semester;


    $scope.$on('typeahead-updated', function () {
        $scope.loadLecturerSchedule();
    });


    $scope.getReportForLecturer = function () {

        document.location.href = '/Report/GetReportForLecturer?lecturerQuery=' + $scope.lecturer +
                                         '&semester=' + $scope.semester +
                                         '&studyYearId=' + $scope.studyYear +
                                         '&title=' + "sometitle";
    };

    $scope.searchLecturer = function (lecturer, callback) {
        $http
            .get($http.prefix + 'LecturerSchedule/SearchLecturer', { params: { query: lecturer } })
            .success(function (response) {
                callback(response.Items);
                $scope.foundLecturersCount = response.Total;
            });

    };

    $scope.loadLecturerSchedule = function () {
        if ($scope.studyYear !== null && $scope.semesterId !== null && $scope.lecturer !== "") {
            $http
                .get($http.prefix + 'LecturerSchedule/LoadLecturerSchedule',
                    {
                        params: {
                            lecturerQuery: $scope.lecturer,
                            studyYearId: $scope.studyYear,
                            semester: $scope.semester
                        }
                    })
                .success(function (response) {
                    $scope.schedules = response.Schedules;
                    $scope.times = response.Times;
                });
        }
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
}
