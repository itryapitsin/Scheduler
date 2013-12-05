function navbarController($scope) {
    $scope.userName = pageModel.userName;
    
    $scope.$on('settingsUpdated', function (e, userName) {
        $scope.userName = userName;
    });
}