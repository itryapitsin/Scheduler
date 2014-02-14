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

    $scope.canSaveReport = function () {
        if ($scope.currentBuildingId == null || $scope.currentBuildingId == undefined || $scope.currentBuildingId == "")
            return false;
        if ($scope.currentAuditoriumId == null || $scope.currentAuditoriumId == undefined || $scope.currentAuditoriumId == "")
            return false;
        return true;
    }

    $scope.getReportForAuditorium = function () {
        //console.log("getReportForAuditorium");
        //console.log($scope.currentAuditoriumId);
        //console.log($scope.currentBuildingId);

        document.location.href = '/Report/GetReportForAuditorium?auditoriumId={0}&buildingId={1}'
            .replace('{0}', $scope.currentAuditoriumId)
            .replace('{1}', $scope.currentBuildingId);
    };

    function loadAuditoriums() {
        
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetAuditoriums', { params: { buildingId: $scope.currentBuildingId } })
            .success(function (response) {
                //console.log(response.auditoriums);
                $scope.auditoriums = response.auditoriums;
                $scope.times = response.times;
            });
    };

    function loadAuditoriumSchedule() {
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetSchedules', { params: { auditoriumId: $scope.currentAuditoriumId } })
            .success(function (response) {
                $scope.schedules = response;
                console.log($scope.currentAuditoriumId);
                console.log($scope.schedules);
            });
    };
}
