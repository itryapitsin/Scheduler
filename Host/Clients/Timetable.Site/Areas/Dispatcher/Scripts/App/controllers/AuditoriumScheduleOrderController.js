function AuditoriumScheduleOrderController($scope, $http, $controller) {
    $controller('BaseTimetableController', { $scope: $scope });

    $scope.pageModel = pageModel;
}
