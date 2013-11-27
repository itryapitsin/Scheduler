﻿function auditoriumScheduleController($scope, $http, timetableService) {
    $scope.moment = moment;
    $scope.pageModel = pageModel;
    $scope.auditoriums = pageModel.Auditoriums;
    $scope.times = pageModel.Times;
    $scope.schedules = pageModel.Schedules;

    angular.extend($scope, timetableService);

    $scope.loadAuditoriums = function (building) {
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetAuditoriums', { params: { buildingId: building } })
            .success(function (response) {
                $scope.auditoriums = response.Auditoriums;
                $scope.times = response.Times;
            });
    };

    $scope.loadAuditoriumSchedule = function () {
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetSchedules', { params: { auditoriumId: $scope.auditorium } })
            .success(function (response) {
                $scope.schedules = response;
            });
    };
}

auditoriumScheduleController.prototype = baseController;