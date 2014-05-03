function AuditoriumScheduleOrderController($scope, $http, $controller) {
    $controller('BaseTimetableController', { $scope: $scope });

    $scope.pageModel = pageModel;
  

    $scope.building = pageModel.buildingId;
    $scope.auditoriumOrders = pageModel.auditoriumOrders;
    $scope.auditoriumType = pageModel.auditoriumTypeId;
    $scope.auditoriums = pageModel.auditoriums;
    $scope.schedules = pageModel.schedules;
    $scope.times = pageModel.times;
    $scope.time = pageModel.times.filter(function f(element) {
        return element.id == pageModel.timeId;
    })[0];

    //console.log("time");
    //console.log($scope.time);

    $scope.daysOfWeek = [{ id: 1, name: "Понедельник" }, { id: 2, name: "Вторник" }, { id: 3, name: "Среда" }, { id: 4, name: "Четверг" }, { id: 5, name: "Пятница" }, { id: 6, name: "Суббота" }, { id: 7, name: "Воскресенье" }];
    $scope.dayOfWeek = $scope.daysOfWeek[pageModel.dayOfWeek];

    console.log($scope.pageModel);

    $scope.buildingChanged = function () {
        if ($scope.isValid())
            getAuditoriumsAndOrders();
    };

    $scope.auditoriumTypeChanged = function () {
        if ($scope.isValid())
           getAuditoriumsAndOrders();
    };

    $scope.timeChanged = function () {
        if ($scope.isValid())
            getAuditoriumsAndOrders();
    };

    $scope.dayOfWeekChanged = function () {
        if ($scope.isValid())
            getAuditoriumsAndOrders();
    };

    function getAuditoriumsAndOrders() {
        $http
            .get($http.prefix + 'AuditoriumSchedule/GetAuditoriumsAndOrders',
                {
                    params: {
                        buildingId: $scope.building,
                        auditoriumTypeId: $scope.auditoriumType,
                        timeId: $scope.time.id,
                        dayOfWeek: $scope.dayOfWeek.id
                    }
                })
            .success(function (response) {
                $scope.auditoriums = response.auditoriums;
                $scope.auditoriumOrders = response.auditoriumOrders;
                $scope.schedules = response.schedules;
               // console.log("response");
                //console.log($scope.auditoriums);
               // console.log($scope.auditoriumOrders);
               // console.log($scope.schedules);
            });
    }

    $scope.schedulesInAuditorium = function( auditoriumId ) {
        var schedulesIn = [];

        //console.log($scope.schedules.length);

        if($scope.schedules)
        {         
           for (var i = 0; i < $scope.schedules.length; ++i) {
               if ($scope.schedules[i])
                if ($scope.schedules[i].auditoriumId == auditoriumId)
                    schedulesIn.push($scope.schedules[i]);
            }
        }
        return schedulesIn;
    }

    $scope.isValid = function () {
        return $scope.building
            && $scope.auditoriumType
            && $scope.time
            && $scope.dayOfWeek;
    };

    $scope.select = function (event, auditorium) {
        $scope.selectedAuditorium = auditorium;
        $scope.selectedDayOfWeek = $scope.dayOfWeek;

        //console.log($scope.dayOfWeek);
        $scope.selectedTime = { id: $scope.time.id, name: moment($scope.time.start, 'HH:mm:ss').format('HH:mm') + '-' + moment($scope.time.end, 'HH:mm:ss').format('HH:mm') };
        $scope.showDialog('ordering.modal.html');
    };
}

function OrderingDialogController($scope, $rootScope, $http) {

    $scope.ok = function () {
        console.log($scope.selectedAuditorium);
        $scope.hideDialog();
    };

    $scope.cancel = $scope.hideDialog;
}
