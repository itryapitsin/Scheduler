function navbarController($scope, $window) {
    $scope.userName = $window.pageModel.userName;
    
    $scope.$on('settingsUpdated', function (e, userName) {
        $scope.userName = userName;
    });
}