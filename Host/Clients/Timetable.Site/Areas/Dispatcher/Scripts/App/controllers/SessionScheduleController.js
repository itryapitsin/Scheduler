function SessionScheduleController($scope, $http, $controller) {
    $controller('BaseTimetableController', { $scope: $scope });

    $scope.pageModel = pageModel;


    $scope.branches = pageModel.branches;
    $scope.currentBranchId = pageModel.currentBranchId;

    $scope.faculties = pageModel.faculties;
    $scope.currentFacultyId = pageModel.currentFacultyId;

    $scope.courses = pageModel.courses;
    $scope.currentCourseId = pageModel.currentCourseId;
    $scope.tutorials = ["Предмет1", "Предмет2", "Предмет3", "Предмет4", "Предмет5", "Предмет6", "Предмет7", "Предмет8", "Предмет9", "Предмет10", "Предмет11", "Предмет12", "Предмет13", "Предмет14", "Предмет15"];//pageModel.tutorials;

    $scope.periods = ["10.01", "11.01", "12.01", "13.01", "14.01", "15.01", "16.01", "17.01", "18.01", "19.01", "20.01", "21.01", "22.01", "23.01", "24.01", "25.01", "26.01","27.01"];

    $scope.branchChanged = function () {
        $scope.currentFacultyId = null;
        $http
            .post($http.prefix + 'SessionSchedule/BranchChanged', { branchId: $scope.currentBranchId })
            .success(function (response) {
                $scope.faculties = response.faculties;
                $scope.courses = response.courses;
            });
    }
  
}