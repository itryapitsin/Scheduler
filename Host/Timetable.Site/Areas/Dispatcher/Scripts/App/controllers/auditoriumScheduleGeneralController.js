function auditoriumScheduleGeneralController($scope, $http, timetableService) {
    $scope.moment = moment;
    $scope.pageModel = pageModel;
    $scope.building = pageModel.BuildingId;
    $scope.schedules = pageModel.Schedules;
    $scope.auditoriumType = pageModel.AuditoriumTypeId;
    $scope.auditoriums = pageModel.Auditoriums;
    $scope.times = pageModel.Times;
    $scope.studyYear = pageModel.StudyYearId;
    $scope.semester = pageModel.Semester;
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
                $scope.auditoriums = response.Auditoriums;
                $scope.schedules = response.Schedules;
                $scope.times = response.Times;
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
