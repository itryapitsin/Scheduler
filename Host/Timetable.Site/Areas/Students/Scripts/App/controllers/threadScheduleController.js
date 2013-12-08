function ThreadScheduleController($scope, $controller) {

    $controller('BaseTimetable', { $scope: $scope });
    $scope.schedules = [];
}
