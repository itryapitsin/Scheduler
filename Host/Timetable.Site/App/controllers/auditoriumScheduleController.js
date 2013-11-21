function auditoriumScheduleController($scope, $http) {
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
}

auditoriumScheduleController.prototype = baseController;