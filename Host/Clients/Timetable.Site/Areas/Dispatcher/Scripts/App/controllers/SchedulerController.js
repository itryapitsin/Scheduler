function SchedulerController($scope, $modal, $controller, $http, $window) {
    $window.document.title = 'Создание расписания';

    $controller('BaseTimetableController', { $scope: $scope });
    angular.extend($scope, pageModel);

    function findBranch() {
        return $.Enumerable.From($scope.branches)
            .Where(function (item) { return item.id == $scope.currentBranchId; })
            .FirstOrDefault({ name: "<Филиал не выбран>" });
    }

    function findFaculty() {
        return $.Enumerable.From($scope.faculties)
            .Where(function (item) { return item.id == $scope.currentFacultyId; })
            .FirstOrDefault({ name: "<Факультет не выбран>" });
    }

    function findCourse() {
        return $.Enumerable.From($scope.courses)
            .Where(function (item) { return item.id == $scope.currentCourseId; })
            .FirstOrDefault({ name: "<Курс не выбран>" });
    };

    function findGroups() {
        var selectedGroups = $.Enumerable.From($scope.currentGroupIds);
        var result = $.Enumerable.From($scope.groups)
            .Where(function (item) {
                return selectedGroups.Contains(item.id);
            });

        if (result.Any())
            return result.ToArray();
        return [{ code: "<Группа не выбрана>" }];
    };

    function findStudyYear() {
        return $.Enumerable
            .From($scope.studyYears)
            .Where(function (x) { return x.id == $scope.currentStudyYearId; })
            .FirstOrDefault({ name: "<Учебный год не выбран>" });
    };

    function findSemestr() {
        return $.Enumerable
            .From($scope.semesters)
            .Where(function (x) { return x.id == $scope.currentSemesterId; })
            .FirstOrDefault({ name: "<Семестр не выбран>" });
    };

    function findStudyType() {
        return $.Enumerable
            .From($scope.studyTypes)
            .Where(function (x) { return x.id == $scope.currentStudyTypeId; })
            .FirstOrDefault({ name: "<Форма обучения не выбрана>" });
    };



    function loadScheduleInfoesForFaculty() {
        var groupIds = $.Enumerable.From($scope.currentGroups).Select('$.id').ToArray();
        var params = {
            facultyId: $scope.currentFacultyId,
            courseId: $scope.currentCourseId,
            groupIds: groupIds.join(),
            studyYearId: $scope.currentStudyYearId,
            semesterId: $scope.currentSemesterId,
            studyTypeId: $scope.currentStudyTypeId
        };

        if ($scope.currentFacultyId != undefined && $scope.currentFacultyId != null &&
           $scope.currentCourseId != undefined && $scope.currentCourseId != null &&
           $scope.currentStudyYearId != undefined && $scope.currentStudyYearId != null &&
           $scope.currentSemesterId != undefined && $scope.currentSemesterId != null &&
           $scope.currentStudyTypeId != undefined && $scope.currentStudyTypeId != null &&
           groupIds != "") {
            $http
            .get($http.prefix + "Scheduler/GetSchedulesAndInfoes", { params: params })
            .success(function (response) {
                $scope.scheduleInfoes = response.scheduleInfoes;
                $scope.schedules = response.schedules;
            });
        }
    };

    function initThread(newThread) {
        //fall here
        angular.extend($scope, newThread);

        $scope.currentBranch = findBranch();
        $scope.currentFaculty = findFaculty();
        $scope.currentCourse = findCourse();
        $scope.currentGroups = findGroups();
        $scope.currentStudyType = findStudyType();

        console.log("pageModel");
        console.log(pageModel);

        $scope.globalBuilding = pageModel.currentPlaningBuildingId;

        $scope.globalScheduleType = pageModel.currentPlaningScheduleTypeId;

        $scope.globalWeekType = pageModel.currentPlaningWeekTypeId;

        $scope.globalAuditorium = pageModel.currentPlaningAuditoriumId;

        $scope.globalSubGroup = pageModel.currentPlaningSubGroup;
     

    }

    function initTimetableParams(newParams) {
        angular.extend($scope, newParams);

        $scope.semestr = findSemestr();
        $scope.studyYear = findStudyYear();
    }

    $scope.$on('threadChanged', function (e, newThread) {
        initThread(newThread);
        loadScheduleInfoesForFaculty();
    });

    $scope.$on('timetableParamsChanged', function (e, newParams) {
        initTimetableParams(newParams);
        loadScheduleInfoesForFaculty();
    });

    $scope.$on('ticketPlanned', function (e, newParams) {
        delete $scope.draggedScheduleInfo;
        delete $scope.pair;
        delete $scope.dayOfWeek;
        delete $scope.ui;

        $scope.selectedScheduleInfo.hoursPassed += 1;
        $scope.scheduleInfoes.splice($scope.selectedScheduleInfoIndex, 0, $scope.selectedScheduleInfo);

        $scope.schedules.push(newParams.schedule.content);
    });

    $scope.$on('ticketRemoved', function (e, newParams) {

        

        for (var i = 0; i < $scope.schedules.length; ++i) {
            if ($scope.schedules[i].id == newParams.schedule.id) {
                $scope.schedules.splice(i, 1);
                break;
            }
        }

        for (var i = 0; i < $scope.scheduleInfoes.length; ++i) {
            if ($scope.scheduleInfoes[i].id == newParams.schedule.scheduleInfoId)
                $scope.scheduleInfoes[i].hoursPassed -= 1;
        }

        $scope.schedules = $scope.schedules.filter(function (obj) { return !$scope.isRelatedScheduleTicket(obj); });
    });


    $scope.$on('ticketEdited', function (e, newParams) {
        for (var i = 0; i < $scope.schedules.length; ++i) {
            if ($scope.schedules[i].id == newParams.schedule.content.id) {
                $scope.schedules[i] = newParams.schedule.content;
                break;
            }
        }

        //magic
        $($scope.draggedScheduleTicket).addClass("hide");

        $($scope.draggedScheduleTicket)
               .removeClass('hide')
               .css("top", "")
               .css("left", "0");

        delete $scope.draggedScheduleTicket;
    });

    $scope.$watch('currentGroups', function () {
        if (!$scope.currentGroups)
            return;

        $scope.currentGroupNames = $.Enumerable
            .From($scope.currentGroups)
            .Select("$.code")
            .Aggregate(function (a, b) {
                return a + ", " + b;
            });
    });

    $scope.showThreadDialog = function () {
        $scope.showDialog('thread.modal.html');
    };

    $scope.showTimetableParamsDialog = function () {
        $scope.showDialog('timetableparams.modal.html');
    };

    $scope.edit = function () {
        //set parameters of schedule

        $scope.building = $scope.selectedTicket.buildingId;
        $scope.pair = $scope.selectedTicket.pair;
        $scope.auditorium = $scope.selectedTicket.auditoriumId;
        $scope.subGroup = $scope.selectedTicket.subGroup;
        $scope.scheduleType = $scope.selectedTicket.scheduleTypeId;

        $scope.weekType = $scope.selectedTicket.weekTypeId;
        $scope.dayOfWeek = $scope.selectedTicket.dayOfWeek;

        //transfer it in other place!
        //update times and auditoriums

        console.log("bc1");
        $http
            .get($http.prefix + "Scheduler/BuildingChanged", {
                params: {
                    buildingId: $scope.building,
                    dayOfWeek: $scope.dayOfWeek,
                    pair: $scope.pair,
                    weekTypeId: $scope.weekType,
                    scheduleInfoId: $scope.selectedTicket == undefined ? $scope.selectedScheduleInfo.id : $scope.selectedTicket.scheduleInfoId,
                    subGroup: $scope.subGroup,
                    scheduleTypeId: $scope.scheduleType,
                    scheduleId: $scope.selectedTicket == undefined ? null : $scope.selectedTicket.id,
                }
            })
            .success(function (response) {
                $scope.times = response.times;
                $scope.auditoriums = response.auditoriums;

                //getTimeForPair();
                $scope.time = $.Enumerable.From($scope.times)
                    .Where(function (item) { return item.position == $scope.pair; })
                    .FirstOrDefault();
            });

        //broadcast not work correct
        $scope.$broadcast('ticketEditing', $scope.selectedTicket);
        $scope.showDialog('planing.modal.html');

    };

    $scope.unplan = function () {

        var ticketName = $scope.selectedTicket.tutorialName + " " + $scope.selectedTicket.lecturerName;

        $scope.confirm = {
            content: "Вы действительно хотите снять занятие '{0}' ?".replace('{0}', ticketName),
            ok: function () {

                var params = {
                    scheduleId: $scope.selectedTicket.id,
                };

                $http
                    .post($http.prefix + 'Scheduler/Remove', params)
                    .success(function (response) {
                        if (response.ok) {

                            $scope.hideDialog();

                            //TODO: make a updatating ui after deleting ? without reloading ?

                            $scope.$broadcast('ticketRemoved', {
                                schedule: $scope.selectedTicket
                            });


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
    };

    $scope.select = function (e, item) {
        if (!$scope.isRelatedScheduleTicket(item)) {
            $scope.selectedTicket = item;
            $scope.schedules = $scope.schedules.filter(function (obj) { return !$scope.isRelatedScheduleTicket(obj); });

            $scope.selectedScheduleInfo = undefined;

            console.log($scope.selectedTicket);

            $http.get($http.prefix + "Scheduler/GetSchedulesForSchedule", {
                params: {
                    scheduleId: $scope.selectedTicket.id,
                }
            }).success(function (response) {


                $scope.relatedSchedules = response;
                console.log("relatedSchedulesWithSchedule1");


                $scope.relatedSchedules = $scope.relatedSchedules.filter(function (obj1) {
                    return $scope.schedules.filter(function (obj2) {
                        return obj1.id == obj2.id || (obj1.dayOfWeek == obj2.dayOfWeek &&
                                                      obj1.pair == obj2.pair &&
                                                     (obj2.weekTypeName == 'Л' || obj1.weekTypeName == obj2.weekTypeName || obj1.weekTypeName == 'Л'));
                    }).length == 0;
                });

                for (var i = 0; i < $scope.relatedSchedules.length; ++i) {
                    $scope.schedules.push($scope.relatedSchedules[i]);
                }
                console.log($scope.relatedSchedules);
            });
        }
    };

    $scope.selectScheduleInfo = function (e, item) {

        $scope.schedules = $scope.schedules.filter(function (obj) { return !$scope.isRelatedScheduleTicket(obj); });

        $scope.selectedTicket = undefined;
        $scope.selectedScheduleInfo = item;

        $http.get($http.prefix + "Scheduler/GetSchedulesForScheduleInfo", {
            params: {
                scheduleInfoId: item.id,
            }
        }).success(function (response) {



            $scope.relatedSchedules = response;
            console.log("relatedSchedulesWithScheduleInfo1");

            $scope.relatedSchedules = $scope.relatedSchedules.filter(function (obj1) {
                return $scope.schedules.filter(function (obj2) {
                    return obj1.id == obj2.id || (obj1.dayOfWeek == obj2.dayOfWeek &&
                                                  obj1.pair == obj2.pair &&
                                                 (obj2.weekTypeName == 'Л' || obj1.weekTypeName == obj2.weekTypeName || obj1.weekTypeName == 'Л'));
                }).length == 0;
            });

            for (var i = 0; i < $scope.relatedSchedules.length; ++i) {
                $scope.schedules.push($scope.relatedSchedules[i]);
            }

            console.log($scope.relatedSchedules);
        });

    };

    $scope.isRelatedScheduleTicket = function (schedule) {
        return schedule.states.indexOf(1) == -1;
    }

    //maybe transfer it to base timetable controller ?
    $scope.isSelectedScheduleTicket = function (schedule) {
        return schedule == $scope.selectedTicket;
    };

    $scope.isSelectedScheduleInfo = function (scheduleInfo) {
        return scheduleInfo == $scope.selectedScheduleInfo;
    };

    $scope.unschedule = function () {
    };

    $scope.print = function () {
    };

    $scope.isThreadValid = function () {
        return $scope.currentBranchId
            && $scope.currentFacultyId
            && $scope.currentCourseId
            && $scope.currentCourseId
            && $scope.currentStudyYearId;
    };

    $scope.stopDragging = function (e, ui, item) {

        //is a schedule info
        if (item.time == undefined)
            $("#test1").css("overflow-y", "scroll");

    };

    $scope.startDragging = function (e, ui, item) {
        //if item not schedule ticket
        if (item.time == undefined) {
            $("#test1").css("overflow", "");
            if ($scope.selectedScheduleInfo != item) {
                $scope.selectScheduleInfo(null, item);
            }
        } else {
            if ($scope.selectedTicket != item) {
                if (!$scope.isRelatedScheduleTicket(item)) {
                    $scope.select(null, item);
                }
            }
        }
    };




    $scope.startPlaning = function (e, ui, scope, pair, dayOfWeek) {

        //TODO: add if case for schedule-ticket and for shedule info

        $scope.pair = pair;
        $scope.dayOfWeek = dayOfWeek;
        $scope.ui = ui;

        $scope.showDialog('planing.modal.html');

        if ($.inArray('schedule-info-card', ui.draggable[0].classList) > -1) {
            //if schedule info planning

            console.log($scope.globalBuilding);
            console.log($scope.globalScheduleType);
            console.log($scope.globalWeekType);
            console.log($scope.globalAuditorium);

            $scope.auditorium = $scope.globalAuditorium == undefined ? undefined : $scope.globalAuditorium;
            $scope.subGroup = $scope.globalSubGroup == undefined ? undefined : $scope.globalSubGroup;
            $scope.scheduleType = $scope.globalScheduleType == undefined ? undefined : $scope.globalScheduleType;
            $scope.weekType = $scope.globalWeekType == undefined ? undefined : $scope.globalWeekType;

            if ($scope.globalBuilding != undefined) {
                $scope.building = $scope.globalBuilding;

                $http
                .get($http.prefix + "Scheduler/BuildingChanged", {
                    params: {
                        buildingId: $scope.building,
                        dayOfWeek: $scope.dayOfWeek,
                        pair: $scope.pair,
                        weekTypeId: $scope.weekType,
                        scheduleInfoId: $scope.selectedTicket == undefined ? $scope.selectedScheduleInfo.id : $scope.selectedTicket.scheduleInfoId,
                        subGroup: $scope.subGroup,
                        scheduleTypeId: $scope.scheduleType,
                        scheduleId: $scope.selectedTicket == undefined ? null : $scope.selectedTicket.id,
                    }
                })
                .success(function (response) {
                    $scope.times = response.times;
                    $scope.auditoriums = response.auditoriums;

                    //getTimeForPair();
                    $scope.time = $.Enumerable.From($scope.times)
                        .Where(function (item) { return item.position == $scope.pair; })
                        .FirstOrDefault();
                });
            } else {
                $scope.building = undefined;
            }

            $scope.isSIplanning = true;
            $("#test1").css("overflow-y", "scroll");

            for (var i = 0; i < $scope.scheduleInfoes.length; ++i)
                if ($scope.scheduleInfoes[i] == $scope.selectedScheduleInfo) {
                    $scope.selectedScheduleInfoIndex = i;
                    break;
                }

            $scope.scheduleInfoes = $scope.scheduleInfoes.filter(function (obj) {
                return obj !== $scope.selectedScheduleInfo;
            });

            //TODO: make a updatating ui after planning ? without reloading ?
        } else {
            //if schedule replanning
            $scope.isSIplanning = false;


            //set parameters of schedule

            $scope.building = $scope.selectedTicket.buildingId;
            $scope.auditorium = $scope.selectedTicket.auditoriumId;
            $scope.subGroup = $scope.selectedTicket.subGroup;
            $scope.scheduleType = $scope.selectedTicket.scheduleTypeId;
            $scope.weekType = $scope.selectedTicket.weekTypeId;
            $scope.dayOfWeek = dayOfWeek;

            //transfer it in other place!
            //update times and auditoriums

            console.log("bc2");
            $http
                .get($http.prefix + "Scheduler/BuildingChanged", {
                    params: {
                        buildingId: $scope.building,
                        dayOfWeek: $scope.dayOfWeek,
                        pair: $scope.pair,
                        weekTypeId: $scope.weekType,
                        scheduleInfoId: $scope.selectedTicket == undefined ? $scope.selectedScheduleInfo.id : $scope.selectedTicket.scheduleInfoId,
                        subGroup: $scope.subGroup,
                        scheduleTypeId: $scope.scheduleType,
                        scheduleId: $scope.selectedTicket == undefined ? null : $scope.selectedTicket.id,
                    }
                })
                .success(function (response) {
                    $scope.times = response.times;
                    $scope.auditoriums = response.auditoriums;

                    //getTimeForPair();
                    $scope.time = $.Enumerable.From($scope.times)
                        .Where(function (item) { return item.position == $scope.pair; })
                        .FirstOrDefault();
                });

            $scope.draggedScheduleTicket = ui.draggable[0];




            //TODO: make a updatating ui after planning ? without reloading ?
        }

        //self.modal.getTimeForPair();

    };

    $scope.cancelPlaning = function () {
        if ($scope.isSIplanning) {
            $scope.scheduleInfoes.splice($scope.selectedScheduleInfoIndex, 0, $scope.selectedScheduleInfo);
        } else {
            $($scope.draggedScheduleTicket).addClass("hide");
            $($scope.draggedScheduleTicket)
                   .removeClass('hide')
                   .css("top", "")
                   .css("left", "0");
        }
        $scope.hideDialog();
    }

    $scope.isFacultySelected = function () {
        if ($scope.currentFacultyId == null || $scope.currentFacultyId == undefined || $scope.currentFacultyId == "")
            return false;
        return true;
    }

    $scope.isCourseSelected = function () {
        if ($scope.currentCourseId == null || $scope.currentCourseId == undefined || $scope.currentCourseId == "")
            return false;
        return true;
    }

    $scope.isGroupsSelected = function () {
        if ($scope.currentGroupIds == null || $scope.currentGroupIds == undefined || $scope.currentGroupIds == "")
            return false;
        return true;
    }

    $scope.getReportForFaculty = function () {

        if ($scope.isFacultySelected()) {
            document.location.href = $http.prefix + 'Report/GetReportForFaculty?branchId={0}&facultyId={1}&studyYearId={2}&semesterId={3}'
                .replace('{0}', $scope.currentBranchId)
                .replace('{1}', $scope.currentFacultyId)
                .replace('{2}', $scope.currentStudyYearId)
                .replace('{3}', $scope.currentSemesterId);
        }
    };

    $scope.getReportForCourse = function () {
        if ($scope.isCourseSelected()) {
            document.location.href = $http.prefix + 'Report/GetReportForCourse?branchId={0}&facultyId={1}&studyTypeId={2}&courseId={3}&studyYearId={4}&semesterId={5}'
                .replace('{0}', $scope.currentBranchId)
                .replace('{1}', $scope.currentFacultyId)
                .replace('{2}', $scope.currentStudyTypeId)
                .replace('{3}', $scope.currentCourseId)
                .replace('{4}', $scope.currentStudyYearId)
                .replace('{5}', $scope.currentSemesterId);
        }
    };

    $scope.getReportForGroups = function () {
        if ($scope.isGroupsSelected()) {
            document.location.href = $http.prefix + 'Report/GetReportForGroups?branchId={0}&facultyId={1}&courseId={2}&groupIds={3}&studyYearId={4}&semesterId={5}'
                .replace('{0}', $scope.currentBranchId)
                .replace('{1}', $scope.currentFacultyId)
                .replace('{2}', $scope.currentCourseId)
                .replace('{3}', $scope.currentGroupIds)
                .replace('{4}', $scope.currentStudyYearId)
                .replace('{5}', $scope.currentSemesterId);
        }
    };


    if (!$scope.currentBranchId)
        $scope.showThreadDialog();

    initThread();

    initTimetableParams();
}

function ThreadDialogController($scope, $http, $rootScope) {
    $scope.ok = function () {
        $scope.hideDialog();
        $rootScope.$broadcast('threadChanged', {
            currentBranchId: $scope.currentBranchId,
            currentFacultyId: $scope.currentFacultyId,
            currentCourseId: $scope.currentCourseId,
            currentGroupIds: $scope.currentGroupIds,
            currentStudyTypeId: $scope.currentStudyTypeId,
            faculties: $scope.faculties,
            groups: $scope.groups
        });
    };

    $scope.cancel = $scope.hideDialog;

    $scope.changeBranch = function () {
        $http
            .post($http.prefix + 'Scheduler/BranchChanged', { branchId: $scope.currentBranchId })
            .success(function (response) {
                $scope.faculties = response.faculties;
                $scope.courses = response.courses;
                $scope.currentCourseId = null;
                $scope.currentFacultyId = null;
                $scope.currentGroupIds = [];
                $scope.groups = [];
            });
    };

    $scope.changeFaculty = function () {
        $scope.groups = [];
        $scope.currentGroupIds = [];

        if ($scope.currentFacultyId != undefined && $scope.currentFacultyId != null &&
           $scope.currentCourseId != undefined && $scope.currentCourseId != null &&
           $scope.currentStudyTypeId != undefined && $scope.currentStudyTypeId != null) {

            var params = {
                facultyId: $scope.currentFacultyId,
                courseId: $scope.currentCourseId,
                studyTypeId: $scope.currentStudyTypeId
            };


            $http
                .get($http.prefix + 'group/get', { params: params })
                .success(function (response) {
                    $scope.currentGroupIds = [];
                    $scope.groups = response;
                });
        }
    };

    $scope.changeCourse = $scope.changeFaculty;
    $scope.changeStudyType = $scope.changeFaculty;

    $scope.isValid = function () {
        return $scope.groups && $scope.groups.length > 0;
    };
}

function TimetableParamsDialogController($scope, $rootScope) {
    $scope.ok = function () {
        $scope.hideDialog();
        $rootScope.$broadcast('timetableParamsChanged', {
            currentSemesterId: $scope.currentSemesterId,
            currentStudyYearId: $scope.currentStudyYearId,
            currentTimetableTypeId: $scope.currentTimetableTypeId,
        });
    };

    $scope.cancel = function () {
        if ($scope.isValid())
            $scope.hideDialog();
        else
            $scope.message = "Необходимо заполнить поля";
    };

    $scope.isValid = function () {
        return $scope.currentSemesterId && $scope.currentStudyYearId;
    };
}

function PlaningDialogController($scope, $rootScope, $http) {

    $scope.daysOfWeek = [{ id: 1, name: 'Понедельник' }, { id: 2, name: 'Вторник' }, { id: 3, name: 'Среда' }, { id: 4, name: 'Четверг' }, { id: 5, name: 'Пятница' }, { id: 6, name: 'Суббота' }, { id: 7, name: 'Воскресенье' }];
    //$scope.subGroup = '1/2';


    function getTimePeriod() {
        if ($scope.time)
            return moment($scope.time.start, 'HH:mm:ss').format('HH:mm') + ' - ' + moment($scope.time.end, 'HH:mm:ss').format('HH:mm');
        else return '';
    }


    //transfer logic to server ?
    function getTimeForPair() {
        $scope.time = $.Enumerable.From($scope.times)
            .Where(function (item) { return item.position == $scope.pair; })
            .FirstOrDefault();
    };



    //transfer logic to server ?
    function availableWeekTypes() {
        //var exp = $.Enumerable.From($scope.tickets);
        //var query = '$.weekTypeName=="{0}"';
        $scope.availableWeekTypes = $scope.weekTypes;

        /*
        angular.copy($scope.weekTypes, $scope.availableWeekTypes);

        if (exp.Count() > 0) {
            $scope.availableWeekTypes = $.Enumerable.From($scope.availableWeekTypes)
                .Where(function(item) {
                    return item.name != "Л" && !exp.Any(query.replace('{0}', item.name));
                })
                .ToArray();
        }*/
    }


    //transfer logic to server ?
    function availablePairs() {
        $scope.availablePairs = $scope.pairs;
    }


    function availableAuditoriums() {
        $http
             .get($http.prefix + "Scheduler/GetFreeAuditoriums", {
                 params: {
                     buildingId: $scope.building,
                     dayOfWeek: $scope.dayOfWeek,
                     pair: $scope.pair,
                     weekTypeId: $scope.weekType,
                     scheduleInfoId: $scope.selectedTicket == undefined ? $scope.selectedScheduleInfo.id :  $scope.selectedTicket.scheduleInfoId,
                     subGroup: $scope.subGroup,
                     scheduleTypeId: $scope.scheduleType,
                     scheduleId: $scope.selectedTicket == undefined ? null : $scope.selectedTicket.id
                 }
             })
             .success(function (response) {
                 $scope.auditoriums = response;
                 getTimeForPair();
                 console.log("ok");
             });
    }

    $scope.tickets = $scope.findScheduleTickets($scope.pair, $scope.dayOfWeek);
    availableWeekTypes();
    availablePairs();

    console.log("bc3");
    $scope.buildingChanged = function () {

        $rootScope.globalBuilding = $scope.building;


        $http
            .get($http.prefix + "Scheduler/BuildingChanged", {
                params: {
                    buildingId: $scope.building,
                    dayOfWeek: $scope.dayOfWeek,
                    pair: $scope.pair,
                    weekTypeId: $scope.weekType,
                    scheduleInfoId: $scope.selectedTicket == undefined ? $scope.selectedScheduleInfo.id : $scope.selectedTicket.scheduleInfoId,
                    subGroup: $scope.subGroup,
                    scheduleTypeId: $scope.scheduleType,
                    scheduleId: $scope.selectedTicket == undefined ? null : $scope.selectedTicket.id
                }
            })
            .success(function (response) {
                $scope.times = response.times;
                $scope.auditoriums = response.auditoriums;

                getTimeForPair();
            });
    };

    $scope.pairChanged = function () {
        $scope.tickets = $scope.findScheduleTickets($scope.pair, $scope.dayOfWeek);
        getTimeForPair();
        availableWeekTypes();
        availablePairs();
        availableAuditoriums();
    };

    $scope.dayOfWeekChanged = function () {
        availableAuditoriums();
    };

    $scope.scheduleTypeChanged = function () {
        $rootScope.globalScheduleType = $scope.scheduleType;
        availableAuditoriums();
    }

    $scope.AuditoriumChanged = function () {
        $rootScope.globalAuditorium = $scope.auditorium;
        $http
           .get($http.prefix + "Scheduler/AuditoriumChanged", {
               params: {
                   auditoriumId: $scope.auditorium,
               }
           });
    }

    $scope.weekTypeChanged = function () {
        console.log("wt changed");
        $rootScope.globalWeekType = $scope.weekType;
        availableAuditoriums();
    };

    $scope.$watch('time', function () {
        $scope.timePeriod = getTimePeriod();
    });

    $scope.ok = function () {

        $rootScope.globalSubGroup = $scope.subGroup;

        if ($scope.isSIplanning == true) {
            var params = {
                auditoriumId: $scope.auditorium,
                dayOfWeek: $scope.dayOfWeek,
                scheduleInfoId: $scope.selectedScheduleInfo == undefined ? undefined : $scope.selectedScheduleInfo.id,
                timeId: $scope.time == undefined ? undefined : $scope.time.id,
                weekTypeId: $scope.weekType,
                typeId: $scope.scheduleType,
                subGroup: $scope.subGroup
            };

            $http
                .post($http.prefix + 'Scheduler/Create', params)
                .success(function (response) {
                    if (response.ok) {
                        //TODO: cases for schedule info planning and schedule replanning

                        $scope.hideDialog();

                        $rootScope.$broadcast('ticketPlanned', {
                            auditoriumId: $scope.auditorium,
                            dayOfWeek: $scope.dayOfWeek,
                            scheduleInfoId: $scope.selectedScheduleInfo == undefined ? undefined : $scope.selectedScheduleInfo.id,
                            timeId: $scope.time == undefined ? undefined : $scope.time.id,
                            weekTypeId: $scope.weekType,
                            typeId: $scope.scheduleType,
                            schedule: response
                        });
                    }

                    if (response.fail) {
                        $scope.message = response.message;
                    }
                });

        } else {
            var params = {
                auditoriumId: $scope.auditorium,
                dayOfWeek: $scope.dayOfWeek,
                scheduleId: $scope.selectedTicket == undefined ? undefined : $scope.selectedTicket.id,
                timeId: $scope.time == undefined ? undefined : $scope.time.id,
                weekTypeId: $scope.weekType,
                typeId: $scope.scheduleType,
                subGroup: $scope.subGroup
            };

            $http
                .post($http.prefix + 'Scheduler/Edit', params)
                .success(function (response) {
                    if (response.ok) {
                        //TODO: cases for schedule info planning and schedule replanning

                        $scope.hideDialog();

                        $rootScope.$broadcast('ticketEdited', {
                            auditoriumId: $scope.auditorium,
                            dayOfWeek: $scope.dayOfWeek,
                            timeId: $scope.time == undefined ? undefined : $scope.time.id,
                            weekTypeId: $scope.weekType,
                            typeId: $scope.scheduleType,
                            schedule: response
                        });
                    }

                    if (response.fail) {
                        $scope.message = response.message;
                    }
                });
        }
    };

    $scope.cancel = $scope.cancelPlaning;
}

