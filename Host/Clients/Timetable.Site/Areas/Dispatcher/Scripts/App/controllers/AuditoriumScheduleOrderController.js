function AuditoriumScheduleOrderController($scope, $http, $controller) {
    $controller('BaseTimetableController', { $scope: $scope });

    $scope.pageModel = pageModel;
  
    $scope.building = pageModel.buildingId;
    $scope.auditoriumOrders = pageModel.auditoriumOrders;
    $scope.auditoriumType = pageModel.auditoriumTypeId;
    $scope.auditoriums = pageModel.auditoriums;
    $scope.schedules = pageModel.schedules;
    $scope.times = pageModel.times;
    $scope.dt = new Date(pageModel.date);

    $scope.time = pageModel.times.filter(function f(element) {
        return element.id == pageModel.timeId;
    })[0];

    console.log("auditoriumOrders ");
    console.log($scope.auditoriumOrders);

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

    $scope.dateChanged = function () {
        if ($scope.isValid())
            getAuditoriumsAndOrders();
    };

    function getAuditoriumsAndOrders() {

        console.log($scope.dt);

        $http
            .get($http.prefix + 'AuditoriumSchedule/GetAuditoriumsAndOrders',
                {
                    params: {
                        buildingId: $scope.building,
                        auditoriumTypeId: $scope.auditoriumType,
                        timeId: $scope.time.id,
                        date: moment($scope.dt).format("YYYY-MM-DD")
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
            && $scope.dt;
    };

    $scope.select = function (event, auditorium) {
        if (event.originalEvent.toElement.localName !== "a") {
            $scope.selectedAuditorium = auditorium;
            $scope.selectedDate = $scope.dt;
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
        $scope.selectedDate = $scope.dt;
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

    //start datepicker
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    // Disable weekend selection
    $scope.disabled = function (date, mode) {
        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    };

    $scope.toggleMin = function () {
        $scope.minDate = $scope.minDate ? null : new Date();
    };
    $scope.toggleMin();

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    $scope.initDate = $scope.dt;
    $scope.formats = ['yyyy-MM-dd', 'dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];
    //end datepicker
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
                            AutoDelete: $scope.currentAutoDeleteState,
                            Date: moment($scope.selectedDate).format("YYYY-MM-DD"),
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
