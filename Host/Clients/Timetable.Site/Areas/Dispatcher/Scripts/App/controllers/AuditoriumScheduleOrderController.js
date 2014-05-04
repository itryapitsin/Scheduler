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

    console.log("auditoriumOrders ");
    console.log($scope.auditoriumOrders);

    $scope.daysOfWeek = [{ id: 1, name: "Понедельник" }, { id: 2, name: "Вторник" }, { id: 3, name: "Среда" }, { id: 4, name: "Четверг" }, { id: 5, name: "Пятница" }, { id: 6, name: "Суббота" }, { id: 7, name: "Воскресенье" }];
    $scope.dayOfWeek = $scope.daysOfWeek[pageModel.dayOfWeek-1];

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

    $scope.ordersInAuditorium = function (auditoriumId) {
        var ordersIn = [];

        //console.log($scope.schedules.length);

        if ($scope.auditoriumOrders) {
            for (var i = 0; i < $scope.auditoriumOrders.length; ++i) {
                if ($scope.auditoriumOrders[i])
                    if ($scope.auditoriumOrders[i].auditoriumId == auditoriumId)
                        ordersIn.push($scope.auditoriumOrders[i]);
            }
        }
        return ordersIn;
    }

    $scope.deleteAuditoriumOrder = function (order) {
        for (var i = $scope.auditoriumOrders.length - 1; i >= 0; i--) {
            if ($scope.auditoriumOrders[i].id == order.id) {
                $scope.auditoriumOrders.splice(i, 1);
                break;
            }
        }
    }

    $scope.editAuditoriumOrder = function (order) {
        for (var i = $scope.auditoriumOrders.length - 1; i >= 0; i--) {
            if ($scope.auditoriumOrders[i].id == order.id) {
                $scope.auditoriumOrders[i] = order;
                break;
            }
        }
    }

    $scope.addAuditoriumOrder = function (order){
        $scope.auditoriumOrders.push(order);
    }

    $scope.isValid = function () {
        return $scope.building
            && $scope.auditoriumType
            && $scope.time
            && $scope.dayOfWeek;
    };

    $scope.select = function (event, auditorium) {
        if (event.originalEvent.toElement.localName !== "a") {
            $scope.selectedAuditorium = auditorium;
            $scope.selectedDayOfWeek = $scope.dayOfWeek;
            $scope.selectedOrder = undefined;
            //console.log("event");
            //console.log(event.originalEvent.toElement);
            $scope.selectedTime = { id: $scope.time.id, name: moment($scope.time.start, 'HH:mm:ss').format('HH:mm') + '-' + moment($scope.time.end, 'HH:mm:ss').format('HH:mm') };
            $scope.showDialog('ordering.modal.html');
        }
    };

    $scope.editOrder = function (event, auditorium, order) {
        $scope.selectedOrder = order;
        $scope.selectedAuditorium = auditorium;
        $scope.selectedDayOfWeek = $scope.dayOfWeek;
        $scope.selectedTime = { id: $scope.time.id, name: moment($scope.time.start, 'HH:mm:ss').format('HH:mm') + '-' + moment($scope.time.end, 'HH:mm:ss').format('HH:mm') };

        $scope.showDialog('ordering.modal.html');
    }

    $scope.deleteOrder = function (event, order) {

        //console.log("order");
        //console.log(order);

        $scope.confirm = {
            content: "Вы действительно хотите удалить заявку '{0}' ?".replace('{0}', ""),
            ok: function () {

                var params = {
                    orderId: order.id,
                };

                $http
                    .post($http.prefix + 'AuditoriumSchedule/UnOrderAuditorium', params)
                    .success(function (response) {
                        if (response.ok) {
                            $scope.deleteAuditoriumOrder(order);
                            $scope.hideDialog();
                        }

                        if (response.fail) {
                            $scope.message = response.message;
                        }
                    });

                $scope.hideDialog();
            },
            cancel: function () {
                $scope.hideDialog();
            }
        };
        $scope.showDialog('confirmation.html');

    }
}

function OrderingDialogController($scope, $rootScope, $http) {

    $scope.lecturerName = $scope.selectedOrder == undefined ? undefined : $scope.selectedOrder.lecturerName;
    $scope.tutorialName = $scope.selectedOrder == undefined ? undefined : $scope.selectedOrder.tutorialName;
    $scope.threadName = $scope.selectedOrder == undefined ? undefined : $scope.selectedOrder.threadName;

    $scope.currentAutoDeleteState = $scope.selectedOrder == undefined ? false : $scope.selectedOrder.autoDelete;

    if ($scope.selectedOrder !== undefined) {
        console.log("AutoDelete");
        console.log($scope.selectedOrder.autoDelete);
    }


    $scope.autoDeletes = [{ state: true, name: "Да" }, { state: false, name: "Нет" }];

    $scope.ok = function () {
        if ($scope.selectedOrder == undefined) {
            $http
                .get($http.prefix + 'AuditoriumSchedule/OrderAuditorium',
                    {
                        params: {
                            LecturerName: $scope.lecturerName,
                            TutorialName: $scope.tutorialName,
                            ThreadName: $scope.threadName,
                            AuditoriumId: $scope.selectedAuditorium.id,
                            TimeId: $scope.selectedTime.id,
                            DayOfWeek: $scope.selectedDayOfWeek.id,
                            AutoDelete: $scope.currentAutoDeleteState
                        }
                    })
                .success(function (response) {
                    console.log("success plan order");
                    console.log(response.content);
                    $scope.addAuditoriumOrder(response.content);
                });
        } else {
            $http
                .get($http.prefix + 'AuditoriumSchedule/EditOrderAuditorium',
                    {
                        params: {
                            AuditoriumOrderId: $scope.selectedOrder.id,
                            LecturerName: $scope.lecturerName,
                            TutorialName: $scope.tutorialName,
                            ThreadName: $scope.threadName,
                            AutoDelete: $scope.currentAutoDeleteState
                        }
                    })
                .success(function (response) {
                    console.log("success edit order");

                    $scope.selectedOrder.lecturerName = $scope.lecturerName;
                    $scope.selectedOrder.tutorialName = $scope.tutorialName;
                    $scope.selectedOrder.threadName = $scope.threadName;
                    $scope.selectedOrder.autoDelete = $scope.currentAutoDeleteState;

                    $scope.editAuditoriumOrder($scope.selectedOrder);
                });
        }
        $scope.hideDialog();
    };

    $scope.cancel = $scope.hideDialog;
}
