function AuditoriumScheduleController($scope, $http, $controller) {
    $controller('BaseTimetableController', { $scope: $scope });

    $scope.pageModel = pageModel;
    $scope.building = pageModel.buildingId;
    $scope.auditorium = pageModel.auditoriumId;
    $scope.auditoriums = pageModel.auditoriums;
    $scope.times = pageModel.times;
    $scope.studyYear = pageModel.studyYearId;
    $scope.semester = pageModel.semester;
    $scope.schedules = pageModel.schedules;
    

    $scope.getReportForAuditorium = function () {

        document.location.href = $http.prefix + 'Report/GetReportForAuditorium?auditoriumId={0}&semester={1}&studyYearId={2}&title="sometitle"'
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
}
