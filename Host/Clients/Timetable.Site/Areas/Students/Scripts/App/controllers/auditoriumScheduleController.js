function AuditoriumScheduleController($scope, $http, $controller, $cookieStore, $window) {
    $window.document.title = 'Расписание аудиторий';

    $controller('BaseTimetableController', { $scope: $scope });
    angular.extend($scope, pageModel);

    $scope.currentAuditoriumId = $cookieStore.get('currentAuditoriumId');
    $scope.currentBuildingId = $cookieStore.get('currentBuildingId');

    if ($scope.currentBuildingId) {
        $scope.currentBuildingId = parseInt($scope.currentBuildingId);
    }

    if ($scope.currentAuditoriumId) {
        $scope.currentAuditoriumId = parseInt($scope.currentAuditoriumId);
    }

    $scope.buildingChanged = function () {
        $cookieStore.put('currentBuildingId', $scope.currentBuildingId);
        loadAuditoriums();
    };

    $scope.auditoriumChanged = function () {
        $cookieStore.put('currentAuditoriumId', $scope.currentAuditoriumId);
        loadAuditoriumSchedule();
    };

    function loadAuditoriums() {
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetAuditoriums', { params: { buildingId: $scope.currentBuildingId } })
            .success(function (response) {
                $scope.auditoriums = response.auditoriums;
                $scope.times = response.times;
            });
    };

    function loadAuditoriumSchedule() {
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetSchedules', { params: { auditoriumId: $scope.currentAuditoriumId } })
            .success(function (response) {
                $scope.schedules = response;
            });
    };
}
