function SchedulerController($scope, $modal, $controller, $http) {
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

    function loadScheduleInfoesForFaculty() {
        var groupIds = $.Enumerable.From($scope.currentGroups).Select('$.id').ToArray();
        var params = {
            facultyId: $scope.currentFacultyId,
            courseId: $scope.currentCourseId,
            groupIds: groupIds,
            studyYearId: $scope.currentStudyYearId,
            semesterId: $scope.currentSemesterId
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

    $scope.$watch('currentGroups', function () {
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
    };

    $scope.unschedule = function () {
    };

    $scope.print = function () {
    };

    $scope.isThreadValid = function () {
        return $scope.currentBuildingId
            && $scope.currentFacultyId
            && $scope.currentCourseId;
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

    if ($scope.currentBuildingId == null)
        $scope.showThreadDialog();
    else
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
        });
    };

    $scope.cancel = $scope.hideDialog;

    $scope.changeBranch = function () {
        $http
            .get($http.prefix + 'faculty/get', { params: { branchId: $scope.currentBranchId } })
            .success(function (response) {
                $scope.faculties = response;
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
        };

        $http
            .get($http.prefix + 'group/get', { params: params })
            .success(function (response) {
                $scope.currentGroupIds = [];
                $scope.groups = response;
            });
    };

    $scope.changeCourse = $scope.changeFaculty;
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

    $scope.cancel = $scope.hideDialog;
}

function PlaningDialogController($scope, $http, $rootScope) {


}

