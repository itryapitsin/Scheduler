app.factory('threadModel', function ($http, $resource, localStorageService, $modal, $q) {

    function modal() {
        this.changeBranch = function () {
            $http
                .get($http.prefix + 'faculty/get', { params: { branchId: self.branch } })
                .success(function (response) {
                    self.faculties = response;
                    delete self.faculty;
                    delete self.selectedGroups;
                    delete self.groups;
                });
        };

        this.changeFaculty = function () {
            delete self.groups;
            delete self.selectedGroups;
            var params = {
                facultyId: self.faculty,
                courseId: self.course,
            };

            $http
                .get($http.prefix + 'group/get', { params: params })
                .success(function (response) {
                    self.groups = response;
                });
        };

        this.changeCourse = function () {
            delete self.selectedGroups;
            delete self.groups;
            var params = {
                facultyId: self.faculty,
                courseId: self.course,
            };

            $http
                .get($http.prefix + 'group/get', { params: params })
                .success(function (response) {
                    self.groups = response;
                });
        };

        var self = this;
        this.saved = false;
        this.courses = pageModel.Courses;
        this.branches = pageModel.Branches;
        this.faculties = pageModel.Faculties;
        this.groups = pageModel.Groups;
        this.branch = pageModel.CurrentBranchId;
        this.faculty = pageModel.CurrentFacultyId;
        this.course = pageModel.CurrentCourseId;
        this.selectedGroups = pageModel.CurrentGroupIds;

        this.isValid = function () {
            return this.branch
                && this.faculty
                && this.course;
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
                //Add alarm
                return;

            s.branch = self.branch;
            s.faculty = self.faculty;
            s.course = self.course;
            s.selectedGroups = self.selectedGroups;

            var param = {
                BranchId: self.branch,
                FacultyId: self.faculty,
                CourseId: self.course,
                GroupIds: self.selectedGroups
            };

            $http
                .get($http.prefix + 'user/savestate', { params: param })
                .success(function (response) {
                    pageModel.ScheduleInfoes = response;
                });

            s.onChanged();

            this.close();
        };
    }

    function thread() {
        var self = this;
        this.branch = pageModel.CurrentBranchId;
        this.faculty = pageModel.CurrentFacultyId;
        this.course = pageModel.CurrentCourseId;
        this.selectedGroups = pageModel.CurrentGroupIds;

        this.modal = new modal();

        this.changedEvent = null;
        this.onChanged = function () {
            if (this.changedEvent)
                this.changedEvent(this);

            this.branchName = this.getBranch().Name;
            this.facultyName = this.getFaculty().Name;
            this.courseName = this.getCourse().Name;
            this.groupNames = $.Enumerable
                .From(this.getGroups())
                .Select("$.Code")
                .Aggregate(function(a, b) {
                    return a + ", " + b;
                });
        };

        this.isValid = function () {
            return this.branch
                && this.faculty
                && this.course;
        };

        this.getBranch = function () {
            return $.Enumerable.From(self.modal.branches)
                .Where(function (item) { return item.Id == self.branch; })
                .FirstOrDefault({ Name: "<Филиал не выбран>" });
        };

        this.getFaculty = function () {
            return $.Enumerable.From(self.modal.faculties)
                .Where(function (item) { return item.Id == self.faculty; })
                .FirstOrDefault({ Name: "<Факультет не выбран>" });
        };

        this.getCourse = function () {
            return $.Enumerable.From(self.modal.courses)
                .Where(function (item) { return item.Id == self.course; })
                .FirstOrDefault({ Name: "<Курс не выбран>" });
        };

        this.getGroups = function () {
            var selectedGroups = $.Enumerable.From(self.selectedGroups);

            var result = $.Enumerable.From(self.modal.groups)
                .Where(function(item) {
                    return selectedGroups.Contains(item.Id);
                });

            if (result.Any())
                return result.ToArray();
            return [{ Code: "<Группа не выбрана>" }];
        };

        this.init = function () {
            if (this.isValid())
                this.onChanged();
        };
    }

    return new thread();
});