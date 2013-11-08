function settings() {
    this.building = { Name: "<Корпус не выбран>" };
    this.weekType = { Name: "<Тип недели не выбран>" };
    this.studyYear = { Name: "<Учебный год не выбран>" };
    this.semestr = { Name: "<Семестр не выбран>" },
    this.changedEvent = null;
    this.onChanged = function () {
        if (this.changedEvent)
            this.changedEvent(this);
    };
    this.isValid = function() {
        return this.building.Id
            && this.weekType.Id
            && this.studyYear.Id
            && this.semestr.Id;
    };
}

function thread() {
    this.branch = { Name: "<Филиал не выбран>" };
    this.faculty = { Name: "<Факультет не выбран>" };
    this.courses = [{ Name: "<Курс не выбрана>" }];
    this.specialities = [{ Name: "<Специальность не выбрана>" }];
    this.groups = [{ Code: "<Группа не выбрана>" }];
    this.changedEvent = null;
    this.onChanged = function () {
        if (this.changedEvent)
            this.changedEvent(this);
    };
    this.isValid = function() {
        return this.branch.Id
            && this.faculty.Id
            && this.courses.length > 0
            && this.groups.length > 0;
    };
}

function settingsModal($scope) {
    var self = this;

    this.semestrs = [
        { Id: 1, Name: "Первый семестр" },
        { Id: 2, Name: "Второй семестр" },
        { Id: 3, Name: "Третий семестр" },
        { Id: 4, Name: "Четвертый семестр" }
    ];

    this.apply = function(s) {
        if (s && s.building)
            $scope.settings.building = s.building;
        else
            $scope.settings.building = { Name: "<Корпус не выбран>" };

        if (s && s.weekType)
            $scope.settings.weekType = s.weekType;
        else
            $scope.settings.weekType = { Name: "<Тип недели не выбран>" };

        if (s && s.studyYear)
            $scope.settings.studyYear = s.studyYear;
        else
            $scope.settings.studyYear = { Name: "<Учебный год не выбран>" };

        if (s && s.semestr)
            $scope.settings.semestr = s.semestr;
        else
            $scope.settings.semestr = { Name: "<Семестр не выбран>" };

        $scope.settings.onChanged();
    };
}

function threadModal(
    $scope,
    $http,
    $resource) {
    var self = this;

    this.courses = $scope.pageModel.Courses;

    this.changeBranch = function () {
        $http
            .get($http.prefix + 'api/v1.0/faculty/get', { params: { branchId: self.branch.Id } })
            .success(function (response) {
                self.faculties = response;
            });
    };

    this.changeFaculty = function () {
        $http
            .get($http.prefix + 'api/v1.0/speciality/get', { params: { facultyId: self.faculty.Id } })
            .success(function (response) {
                self.specialities = response;
            });

    };

    this.changeCourses = function () {
        var params = {
            facultyId: self.faculty.Id,
            courseIdsStr: $.Enumerable.From(self.selectedCourses).Select(function (item) {
                return item.Id;
            }).ToArray(),
        };
        
        var transactions = $resource(
                $http.prefix + 'group/get',
                {},
                {
                    get: { method: "GET", isArray: true },
                });

        transactions.get(
            params,
            function (result) {
                self.groups = result;
            });
    };

    this.apply = function (s) {
        if (s && s.branch)
            $scope.thread.branch = s.branch;
        else
            $scope.thread.branch = { Name: "<Филиал не выбран>" };

        if (s && s.faculty)
            $scope.thread.faculty = s.faculty;
        else
            $scope.thread.faculty = { Name: "<Факультет не выбран>" };

        if (s && s.courses)
            $scope.thread.courses = s.selectedCourses;
        else
            $scope.thread.courses = [{ Name: "<Курс не выбрана>" }];

        //if (s && s.specialities)
        //    $scope.thread.specialities = s.selectedSpecialities;
        //else
        //    $scope.thread.specialities = [{ Name: "<Специальность не выбрана>" }];

        if (s && s.groups)
            $scope.thread.groups = s.selectedGroups;
        else
            $scope.thread.groups = [{ Name: "<Группа не выбрана>" }];

        $scope.thread.onChanged();
    };
}

function schedulerController(
    $scope,
    $routeParams,
    $locale,
    $http,
    $resource,
    localStorageService) {

    $scope.pageModel = pageModel;
    $scope.settings = new settings();
    $scope.settings.changedEvent = function() {
        var t = $scope.isValid();
    };
    $scope.thread = new thread();
    $scope.thread.changedEvent = function () {
        if (!this.isValid())
            // Add error message support
            return;
        
        if(!$scope.settings.isValid())
            // Add error message support
            return;
        
        var params = {
            facultyId: this.faculty.Id,
            courseIdsStr: $.Enumerable.From(this.courses).Select(function (item) { return item.Id; }).ToArray(),
            groupIdsStr: $.Enumerable.From(this.groups).Select(function (item) { return item.Id; }).ToArray(),
            studyYearId: $scope.settings.studyYear.Id,
            semestr: $scope.settings.semestr.Id
    };

        var transactions = $resource(
                $http.prefix + 'scheduleInfo/get',
                {},
                {
                    get: { method: "GET", isArray: true },
                });

        transactions.get(
            params,
            function (result) {
                self.groups = result;
            });
    };
    $scope.settingsModal = new settingsModal($scope);
    $scope.threadModal = new threadModal($scope, $http, $resource);

    $scope.isValid = function() {
        return $scope.settings.isValid()
            && $scope.thread.isValid();
    };
}


