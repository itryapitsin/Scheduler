function schedulerController(
    $scope,
    $routeParams,
    $locale,
    $http,
    $resource,
    $modal,
    $q,
    settingsModel,
    threadModel,
    localStorageService,
    facultyService,
    specialityService,
    groupService) {

    $scope.facultyService = facultyService;
    $scope.specialityService = specialityService;
    $scope.groupService = groupService;
    $scope.pageModel = pageModel;
    $scope.settings = settingsModel;
    $scope.thread = threadModel;
    $scope.thread.changedEvent = function () {
        
    };
    $scope.isValid = function() {
        return $scope.settings.isValid()
            && $scope.thread.isValid();
    };
}

schedulerController.prototype = baseController;