function ThreadScheduleController($scope, $controller, $cookieStore, $window, $http) {
    $window.document.title = 'Расписание групп';

    $controller('BaseTimetableController', { $scope: $scope });
    angular.extend($scope, pageModel);
    $scope.pairs = [1, 2, 3, 4, 5, 6, 7, 8];

    $scope.currentBranchId = $cookieStore.get('currentBranchId');
    if ($scope.currentBranchId) {
        $scope.currentBranchId = parseInt($scope.currentBranchId);
    }

    $scope.currentStudyTypeId = $cookieStore.get('currentStudyTypeId');
    if ($scope.currentStudyTypeId) {
        $scope.currentStudyTypeId = parseInt($scope.currentStudyTypeId);
    }

    $scope.currentFacultyId = $cookieStore.get('currentFacultyId');
    if ($scope.currentFacultyId) {
        $scope.currentFacultyId = parseInt($scope.currentFacultyId);
    }

    $scope.currentCourseId = $cookieStore.get('currentCourseId');
    if ($scope.currentCourseId) {
        $scope.currentCourseId = parseInt($scope.currentCourseId);
    }

    $scope.currentGroupId = $cookieStore.get('currentGroupId');
    if ($scope.currentGroupId) {
        $scope.currentGroupId = parseInt($scope.currentGroupId);
    }

    $scope.$watch('currentBranchId', function () {
        if ($scope.currentBranchId)
            $cookieStore.put('currentBranchId', $scope.currentBranchId);
        else
            $cookieStore.remove('currentBranchId');
    });

    $scope.$watch('currentFacultyId', function () {
        if ($scope.currentFacultyId)
            $cookieStore.put('currentFacultyId', $scope.currentFacultyId);
        else
            $cookieStore.remove('currentFacultyId');
    });

    $scope.$watch('currentGroupId', function () {
        if ($scope.currentGroupId)
            $cookieStore.put('currentGroupId', $scope.currentGroupId);
        else
            $cookieStore.remove('currentGroupId');
    });

    $scope.$watch('currentStudyTypeId', function () {
        if ($scope.currentStudyTypeId)
            $cookieStore.put('currentStudyTypeId', $scope.currentStudyTypeId);
        else
            $cookieStore.remove('currentStudyTypeId');
    });

    $scope.$watch('currentCourseId', function () {
        if ($scope.currentCourseId)
            $cookieStore.put('currentCourseId', $scope.currentCourseId);
        else
            $cookieStore.remove('currentCourseId');
    });

    $scope.canSaveReport = function () {
        if ($scope.currentFacultyId == null || $scope.currentFacultyId == undefined || $scope.currentFacultyId == "")
            return false;
        if ($scope.currentCourseId == null || $scope.currentCourseId == undefined || $scope.currentCourseId == "")
            return false;
        if ($scope.currentGroupId == null || $scope.currentGroupId == undefined || $scope.currentGroupId == "")
            return false;
        return true;
    }

    $scope.getReportForGroup = function () {

        console.log("getReportForGroup");
        console.log($scope.groups);
        //console.log($scope.currentAuditoriumId);
        //console.log($scope.currentBuildingId);

        document.location.href = $http.prefix + 'Report/GetReportForGroup?facultyId={0}&courseId={1}&groupId={2}'
            .replace('{0}', $scope.currentFacultyId)
            .replace('{1}', $scope.currentCourseId)
            .replace('{2}', $scope.currentGroupId);
    };

    function branchChanged() {
        $scope.currentFacultyId = null;

        $http
            .post($http.prefix + 'ThreadSchedule/BranchChanged', { branchId: $scope.currentBranchId })
            .success(function (response) {
                $scope.faculties = response.faculties;
                $scope.courses = response.courses;
            });
    }

    function loadGroups() {
        $scope.currentGroupId = null;
        $scope.groups = [];

        if (!$scope.currentFacultyId || !$scope.currentCourseId)
            return;
        
        $http
            .post($http.prefix + 'ThreadSchedule/GetGroups', {
                facultyId: $scope.currentFacultyId,
                courseId: $scope.currentCourseId,
                studyTypeId: $scope.currentStudyTypeId
            })
            .success(function (response) {
                
                $scope.groups = response;
            });
    }

    function loadThreadSchedule() {
        $http
            .get($http.prefix + 'ThreadSchedule/GetSchedules', {
                params: {
                    facultyId: $scope.currentFacultyId,
                    courseId: $scope.currentCourseId,
                    groupId: $scope.currentGroupId,
                }
            })
            .success(function (response) {
                $scope.schedules = response;
            });
    };

   
    
    /*function loadSchedule() {
        if (!$scope.isValid())
            return;
    }*/

    $scope.branchChanged = function () {
        branchChanged();
        loadGroups();
    };

    $scope.studyFormChanged = loadGroups;
    $scope.facultyChanged = loadGroups;
    $scope.courseChanged = loadGroups;
    $scope.groupChanged = loadThreadSchedule;
    $scope.downloadReport = function() {
    };

    $scope.isValid = function () {
        return $scope.currentBranchId
            && $scope.currentFacultyId
            && $scope.currentCourseId
            && $scope.currentGroupId
            && $scope.currentStudyFormId;
    };
}
