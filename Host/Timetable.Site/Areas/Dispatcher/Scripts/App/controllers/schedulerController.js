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
            groupIds: groupIds,
            studyYearId: $scope.currentStudyYearId,
            semesterId: $scope.currentSemesterId,
            studyTypeId: $scope.currentStudyTypeId
        };

        $http
        .get($http.prefix + "Scheduler/GetSchedulesAndInfoes", { params: params })
        .success(function (response) {
            $scope.scheduleInfoes = response.scheduleInfoes;
            $scope.schedules = response.schedules;
        });
    };

    function initThread(newThread) {
        angular.extend($scope, newThread);

        $scope.currentBranch = findBranch();
        $scope.currentFaculty = findFaculty();
        $scope.currentCourse = findCourse();
        $scope.currentGroups = findGroups();
        $scope.currentStudyType = findStudyType();
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
        $scope.$broadcast('ticketEditing', $scope.selectedTicket);
        $scope.showDialog('planing.modal.html');
    };

    $scope.select = function(e, item) {
        $scope.selectedTicket = item;
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

    $scope.stopDragging = function (e, ui) {
        $("#test1").css("overflow-y", "scroll");
    };

    $scope.startDragging = function (e, ui, item) {
        self.draggedItem = item;

        $("#test1").css("overflow", "");
    };

    $scope.startPlaning = function (e, ui, scope, pair, dayOfWeek) {
        $scope.showDialog('planing.modal.html');

        //self.modal.scheduleInfo = self.draggedItem;
        //self.modal.pair = pair;
        //self.modal.dayOfWeek = dayOfWeek;
        //self.modal.ui = ui;
        //self.modal.getTimeForPair();

        //$(ui.draggable[0]).addClass("hide");
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
    };

    $scope.changeCourse = $scope.changeFaculty;
    $scope.changeStudyType = $scope.changeFaculty;

    $scope.isValid = function() {
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

    $scope.isValid = function() {
        return $scope.currentSemesterId && $scope.currentStudyYearId;
    };
}

function PlaningDialogController($scope,$rootScope, $http) {

    $scope.$on('ticketEditing', function(e, newParams) {
        angular.extend($scope, newParams);
    });

    $scope.ok = function() {
        $scope.hideDialog();
        $rootScope.$broadcast('ticketPlanned', {});
    };

    $scope.cancel = $scope.hideDialog;
}

