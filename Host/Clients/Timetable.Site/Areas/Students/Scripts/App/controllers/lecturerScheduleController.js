function LecturerScheduleController($scope, $http, $controller, $cookieStore, $window) {
    $window.document.title = 'Расписание преподавателей';

    $controller('BaseTimetableController', { $scope: $scope });
    angular.extend($scope, pageModel);
    $scope.pairs = [1, 2, 3, 4, 5, 6, 7, 8];

    $scope.selected = undefined;
    $scope.states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico', 'New York', 'North Dakota', 'North Carolina', 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island', 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont', 'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'];

    //$scope.currentLecturerId = $cookieStore.get('currentLecturerId');
    $scope.currentLecturerSearchString = $cookieStore.get('currentLecturerSearchString');

    //if ($scope.currentLecturerId) {
        //$scope.currentLecturerId = parseInt($scope.currentLecturerId);
    //}

    $scope.lecturerChanged = function () {
        //console.log("lecturerChanged");
        //$cookieStore.put('currentLecturerId', $scope.currentLecturerId);
        $cookieStore.put('currentLecturerSearchString', $scope.currentLecturerSearchString);
    };

    $scope.canFindLecturer = function () {
        if ($scope.currentLecturerSearchString == null || $scope.currentLecturerSearchString == "" || $scope.currentLecturerSearchString == undefined)
            return false;
        return true;
    }

    $scope.canSaveReport = function () {
        if ($scope.schedules == null || $scope.schedules == undefined || $scope.schedules == "")
            return false;
        return true;
    }

    $scope.loadLecturerSchedule = function() {
        //console.log("loadLecturerSchedule");
        $http
            .get($http.prefix + 'LecturerSchedule/LoadLecturerSchedule', { params: { searchString: $scope.currentLecturerSearchString } })
            .success(function (response) {
                $scope.schedules = response;
            });
    }

    $scope.getReportForLecturer = function () {
        console.log("getReportForLecturer");
        //console.log($scope.currentAuditoriumId);
        //console.log($scope.currentBuildingId);

        document.location.href = $http.prefix + 'Report/GetReportForLecturer?lecturerSearchString={0}'
            .replace('{0}', $scope.currentLecturerSearchString);
    };
}