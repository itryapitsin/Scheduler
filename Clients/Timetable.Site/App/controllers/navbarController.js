function navbarController($scope, auth) {
    $scope.model = auth.user();
}