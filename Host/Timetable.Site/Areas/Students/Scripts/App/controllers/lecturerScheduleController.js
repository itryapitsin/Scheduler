function LecturerScheduleController($scope, $http, $controller, $cookieStore, $window) {
    $window.document.title = 'Расписание преподавателей';

    $controller('BaseTimetableController', { $scope: $scope });
    angular.extend($scope, pageModel);

    $scope.currentLecturerId = $cookieStore.get('currentLecturerId');
    if ($scope.currentLecturerId) {
        $scope.currentLecturerId = parseInt($scope.currentLecturerId);
    }

    $scope.lecturerChanged = function () {
        $cookieStore.put('currentLecturerId', $scope.currentLecturerId);
        loadSchedule();
    };

    function findLecturers() {

    }

    function loadSchedule() {
        $http
            .get($http.prefix + 'LecturerSchedule/LoadLecturerSchedule', { params: { lecturerId: $scope.currentLecturerId, } })
            .success(function (response) {
                $scope.schedules = response;
            });
    }
}