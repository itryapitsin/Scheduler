function auditoriumScheduleGeneralController($scope, $http, timetableService) {
    $scope.moment = moment;
    $scope.pageModel = pageModel;
    $scope.building = pageModel.buildingId;
    $scope.schedules = pageModel.schedules;
    $scope.auditoriumType = pageModel.auditoriumTypeId;
    $scope.auditoriums = pageModel.auditoriums;
    $scope.times = pageModel.times;
    $scope.studyYear = pageModel.studyYearId;
    $scope.semester = pageModel.semester;
    $scope.days = [1, 2, 3, 4, 5, 6, 7];
    
    angular.extend($scope, timetableService);

    $scope.buildingChanged = function () {
        loadAuditoriumsAndSchedules();
    };

    $scope.auditoriumTypeChanged = function () {
        loadAuditoriumsAndSchedules();
    };

    $scope.description = function(schedule) {
        $scope.selectedSchedule = schedule;
    };
    
    $scope.studyYearChanged = function () {
        loadAuditoriumsAndSchedules();
    };
    
    $scope.semesterChanged = function () {
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
                $scope.auditoriums = response.auditoriums;
                $scope.schedules = response.schedules;
                $scope.times = response.times;
            });
    }

    $scope.isBusy = function (auditorium, day, time) {
        var hasSchedule = $.Enumerable.From($scope.schedules)
            .Where(function(item) {
                return item.AuditoriumNumber == auditorium.Number && item.DayOfWeek == day && item.TimeId == time.Id;
            })
            .ToArray();

        return hasSchedule.length > 0;
    };
}
