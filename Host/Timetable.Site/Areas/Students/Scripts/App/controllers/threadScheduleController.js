function threadScheduleController(
    $scope,
    timetableService) {
    
    $scope.pageModel = pageModel;
    $scope.moment = moment;
    
    angular.extend($scope, timetableService);
}



threadScheduleController.prototype = baseController;