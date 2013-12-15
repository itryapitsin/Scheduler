function AuditoriumScheduleGeneralController($scope, $http, $controller) {
    $controller('BaseTimetableController', { $scope: $scope });

    $scope.pageModel = pageModel;
    $scope.building = pageModel.buildingId;
    $scope.schedules = pageModel.schedules;
    $scope.auditoriumType = pageModel.auditoriumTypeId;
    $scope.auditoriums = pageModel.auditoriums;
    $scope.times = pageModel.times;
    $scope.studyYear = pageModel.studyYearId;
    $scope.semester = pageModel.semester;
    
    $scope.buildingChanged = function () {
        if ($scope.isValid())
            getAuditoriumsAndSchedules();
    };

    $scope.auditoriumTypeChanged = function () {
        if($scope.isValid())
            getAuditoriumsAndSchedules();
    };

    $scope.description = function(schedule) {
        $scope.selectedSchedule = schedule;
    };
    
    $scope.studyYearChanged = function () {
        if ($scope.isValid())
            getAuditoriumsAndSchedules();
    };
    
    $scope.semesterChanged = function () {
        if ($scope.isValid())
            getAuditoriumsAndSchedules();
    };

    function getAuditoriumsAndSchedules() {
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

    $scope.isBusy = function (auditorium, day, pair) {
        var hasSchedule = $.Enumerable.From($scope.schedules)
            .Where(function(item) {
                return item.auditoriumNumber == auditorium.number && item.dayOfWeek == day && item.pair == pair;
            })
            .ToArray();

        return hasSchedule.length > 0;
    };

    $scope.isValid = function() {
        return $scope.building
            && $scope.auditoriumType
            && $scope.studyYear
            && $scope.semester;
    };
}
