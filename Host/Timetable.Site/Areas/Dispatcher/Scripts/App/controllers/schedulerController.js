function schedulerController(
    $scope,
    settingsModel,
    threadModel,
    scheduleCreatorModel) {
    
    $scope.pageModel = pageModel;
    $scope.settings = settingsModel;
    $scope.thread = threadModel;
    $scope.creator = scheduleCreatorModel;
    $scope.moment = moment;
    $scope.isValid = function() {
        return $scope.settings.isValid()
            && $scope.thread.isValid();
    };

    $scope.settings.init();
    $scope.thread.init();

    $scope.settings.changedEvent = function () {
        $scope.creator.loadScheduleInfoesForFaculty(
            $scope.thread.faculty,
            $scope.thread.course,
            $scope.thread.groups,
            $scope.settings.studyYear,
            $scope.settings.semestr);
    };
    
    $scope.thread.changedEvent = function () {
        $scope.creator.loadScheduleInfoesForFaculty(
            $scope.thread.faculty,
            $scope.thread.course,
            $scope.thread.groups,
            $scope.settings.studyYear,
            $scope.settings.semestr);
    };
    
    if ($scope.pageModel.CurrentBuildingId == null) {
        $scope.settings.modal.show($scope);
    }
}



schedulerController.prototype = baseController;