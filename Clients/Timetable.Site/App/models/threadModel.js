app.factory('threadModel', function ($http, $resource, localStorageService, $modal, $q) {
    
    function modal(savedThread) {
        var self = this;
        this.saved = false;
        this.courses = pageModel.Courses;
        this.branches = pageModel.Branches;
        
        this.isValid = function () {
            return this.branch
                && this.faculty
                && this.courses
                && (this.courses.length > 0)
                && this.groups
                && (this.groups.length > 0);
        };

        this.changeBranch = function () {
            $http
                .get($http.prefix + 'faculty/get', { params: { branchId: self.branch } })
                .success(function (response) {
                    self.faculties = response;
                });

            $http
                .get($http.prefix + 'speciality/GetForBranch', { params: { branchId: self.branch } })
                .success(function (response) {
                    self.specialities = response;
                });
        };

        this.changeFaculty = function () {
            $http
                .get($http.prefix + 'api/v1.0/speciality/get', { params: { facultyId: self.faculty } })
                .success(function (response) {
                    self.specialities = response;
                });

        };

        this.changeCourses = function () {
            var params = {
                facultyId: self.faculty,
                courseIdsStr: self.selectedCourses,
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
        
        this.close = function () {
            $q.when(self.promise).then(function (modalEl) {
                modalEl.modal('hide');
                modalEl.addClass("hide");
            });
        };

        this.show = function ($scope) {
            var modalPromise = $modal({
                template: $http.prefix + 'scheduler/threadmodal',
                persist: true,
                show: false,
                backdrop: 'static',
                scope: $scope
            });

            self.promise = modalPromise;

            $q.when(self.promise).then(function (modalEl) {
                modalEl.modal('show');
                modalEl.removeClass("hide");
            });
        };

        this.apply = function (s) {
            if (!this.isValid())
                return;

            var courses = $.Enumerable.From(self.courses).Select('$.Id');
            var groups = $.Enumerable.From(self.groups).Select('$.Id');

            s.branch = $.Enumerable.From(this.branches).Where(function (item) { return item.Id == self.branch; }).FirstOrDefault({ Name: "<Филиал не выбран>" });
            s.faculty = $.Enumerable.From(this.faculties).Where(function (item) { return item.Id == self.faculty; }).FirstOrDefault({ Name: "<Факультет не выбран>" });
            s.courses = $.Enumerable.From(this.courses)
                .Where(function(item) {
                    return courses.Contains(item.Id);
                }).DefaultIfEmpty([{ Name: "<Курс не выбран>" }]).ToArray();
            
            s.courses = $.Enumerable.From(this.groups)
                .Where(function (item) {
                    return groups.Contains(item);
                }).DefaultIfEmpty([{ Name: "<Группа не выбрана>" }]).ToArray();
            
            var params = {
                facultyId: self.faculty,
                courseIdsStr: self.courses,
                groupIdsStr: self.groups,
                studyYearId: self.studyYear,
                semestr: self.semestr
            };
            
            if (!$scope.settings.isValid())
                // Add error message support
                return;

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

            self.onChanged();
        };
        
        if (savedThread && savedThread.modal) {
            this.branch = savedThread.modal.branch;
            this.changeBranch();

            this.faculty = savedThread.modal.faculty;
            this.changeFaculty();

            this.courses = savedThread.modal.courses;
            this.changeCourses();
        }
    }

    function thread(savedThread) {
        var self = this;
        this.branch = { Name: "<Филиал не выбран>" };
        this.faculty = { Name: "<Факультет не выбран>" };
        this.courses = [{ Name: "<Курс не выбрана>" }];
        this.specialities = [{ Name: "<Специальность не выбрана>" }];
        this.groups = [{ Code: "<Группа не выбрана>" }];
        this.modal = new modal(savedThread);
        
        this.changedEvent = null;
        this.onChanged = function () {
            if (this.changedEvent)
                this.changedEvent(this);
            
            localStorageService.add("thread", this);
        };
        this.isValid = function () {
            return this.branch.Id
                && this.faculty.Id
                && this.courses.length > 0
                && this.groups.length > 0;
        };
        
        this.getBranchName = function () {
            return $.Enumerable
                .From(pageModel.Branches)
                .Where(function (x) { return x.Id == self.studyYear; })
                .FirstOrDefault({ Name: "<Филиал не выбран>" })
                .Name;
        };

        this.getFacultyName = function () {
            return $.Enumerable
                .From(pageModel.Branches)
                .Where(function (x) { return x.Id == self.faculty; })
                .FirstOrDefault({ Name: "<Факультет не выбран>" })
                .Name;
        };
        
        this.getCourseNames = function () {
            return $.Enumerable
                .From(pageModel.Branches)
                .Where(function (x) { return x.Id == self.faculty; })
                .FirstOrDefault({ Name: "<Курс не выбран>" })
                .Name;
        };

        if (savedThread) {
            if (savedThread.branch)
                this.branch = savedThread.branch;

            if (savedThread.faculty)
                this.facult = savedThread.faculty;

            if (savedThread.courses)
                this.courses = savedThread.courses;

            if (savedThread.specialities)
                this.specialities = savedThread.specialities;

            if (savedThread.groups)
                this.groups = savedThread.groups;
        }
    }

    return new thread(
        localStorageService.get("thread"));
});