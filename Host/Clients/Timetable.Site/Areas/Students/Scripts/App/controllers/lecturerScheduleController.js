function LecturerScheduleController($scope, $http, $controller, $cookieStore, $window) {
    $window.document.title = 'Расписание преподавателей';

    $controller('BaseTimetableController', { $scope: $scope });
    angular.extend($scope, pageModel);
    $scope.pairs = [1, 2, 3, 4, 5, 6, 7, 8];

    $scope.currentLecturerId = $cookieStore.get('currentLecturerId');

    $scope.currentLecturer = undefined;

    $scope.loadLecturers = function (val) {

        return $http.get($http.prefix + 'LecturerSchedule/LoadLecturers', {
            params: {
                searchString: val
            }
        }).then(function (res) {
            var lecturers = [];

           
           

            angular.forEach(res.data, function (item) {
               lecturers.push(item);
            });

          
            return lecturers;
        });
    };

    $scope.loadLecturerSchedule = function () {
        $cookieStore.put('currentLecturerId', $scope.currentLecturer.id);

        $http
            .get($http.prefix + 'LecturerSchedule/LoadLecturerSchedule', { params: { LecturerId: $scope.currentLecturer.id} })
            .success(function (response) {
                $scope.schedules = response;
            });
    }

    $scope.getReportForLecturer = function () {
        document.location.href = $http.prefix + 'Report/GetReportForLecturer?lecturerId={0}'
            .replace('{0}', $scope.currentLecturer.id);
    };
}