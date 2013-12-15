function LecturerScheduleController($scope, $http, $controller) {
    $controller('BaseTimetableController', { $scope: $scope });

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

        document.location.href = '/Report/GetReportForLecturer?lecturerQuery={0}&semester={1}&studyYearId={2}&title=sometitle'
            .replace('{0}', $scope.lecturer)
            .replcae('{1}', $scope.semester)
            .replace('{2}', $scope.studyYear);
    };

    $scope.searchLecturer = function (lecturer, callback) {
        $http
            .get($http.prefix + 'LecturerSchedule/SearchLecturer', { params: { query: lecturer } })
            .success(function (response) {
                callback(response.Items);
                $scope.foundLecturersCount = response.total;
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
                    $scope.schedules = response.schedules;
                    $scope.times = response.times;
                });
        }
    };
}
