function logoutController($location, auth) {
    auth.logout();
    
    $location.path('/login');
}