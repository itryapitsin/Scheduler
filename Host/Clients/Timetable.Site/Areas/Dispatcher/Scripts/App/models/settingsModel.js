app.factory('settingsModel', function (localStorageService, $modal, $q, $http) {

    function modal() {
        var self = this;
        this.saved = false;
        this.studyYears = pageModel.StudyYears;
        this.semestrs = pageModel.Semesters;

        this.studyYear = pageModel.CurrentStudyYearId;
        this.semestr = pageModel.CurrentSemesterId;
        this.changedEvent = null;
        this.onChanged = function () {
            if (this.changedEvent)
                this.changedEvent(this);
        };
        
        this.clearErrorMessage = function() {
            delete this.errorMessage;
        };

        this.isValid = function () {
            return this.studyYear
                && this.semestr;
        };

        this.close = function () {
            $q.when(self.promise).then(function (modalEl) {
                modalEl.modal('hide');
                modalEl.addClass("hide");
            });
        };

        this.show = function ($scope) {
            var modalPromise = $modal({
                template: $http.prefix + 'scheduler/timetablesettingsmodal',
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
            if (!this.isValid()) {
                this.errorMessage = "Должны быть указаны все поля";
                return;
            } else {
                this.clearErrorMessage();
            }

            this.close();

            s.studyYear = self.studyYear;
            s.semestr = self.semestr;

            var param = {
                SemesterId: self.semestr,
                WeekTypeId: self.weekType
            };

            $http
                .get($http.prefix + 'user/savestate', { params: param })
                .success(function (response) {
                    pageModel.Times = response;
                });

            self.onChanged();
        };
    }

    function settings() {
        var self = this;
        this.modal = new modal();

        this.changedEvent = null;
        this.onChanged = function () {
            if (this.changedEvent)
                this.changedEvent(this);
        };
        this.isValid = function () {
            return this.weekType
                && this.studyYear
                && this.semestr;
        };

        this.getStudyYearName = function () {
            return $.Enumerable
                .From(pageModel.StudyYears)
                .Where(function (x) { return x.Id == self.studyYear; })
                .FirstOrDefault({ Name: "<Учебный год не выбран>" })
                .Name;
        };

        this.getSemesterName = function () {
            return $.Enumerable
                .From(self.modal.semestrs)
                .Where(function (x) { return x.Id == self.semestr; })
                .FirstOrDefault({ Name: "<Семестр не выбран>" })
                .Name;
        };

        this.init = function () {
            this.studyYear = pageModel.CurrentStudyYearId;
            this.semestr = pageModel.CurrentSemesterId;

            this.onChanged();
        };
    }

    return new settings();
});