function threadScheduleController(
    $scope,
    timetableService) {
    
    $scope.pageModel = pageModel;
    $scope.moment = moment;
    $scope.schedules = [];
    
    angular.extend($scope, timetableService);
}

threadScheduleController.prototype = baseController;