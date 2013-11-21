app.factory('settingsModel', function (localStorageService, $modal, $q, $http) {

    function modal() {
        var self = this;
        this.saved = false;
        this.studyYears = pageModel.StudyYears;
        this.buildings = pageModel.Buildings;
        this.weekTypes = pageModel.WeekTypes;
        this.semestrs = pageModel.Semesters;

        this.studyYear = pageModel.CurrentStudyYearId;
        this.building = pageModel.CurrentBuildingId;
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
                && this.building
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

            s.building = self.building;
            s.weekType = self.weekType;
            s.studyYear = self.studyYear;
            s.semestr = self.semestr;

            var param = {
                buildingId: self.building,
                StudyYearId: self.studyYear,
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
            return this.building
                && this.weekType
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

        this.getBuildingName = function () {
            return $.Enumerable
                .From(pageModel.Buildings)
                .Where(function (x) { return x.Id == self.building; })
                .FirstOrDefault({ Name: "<Корпус не выбран>" })
                .Name;
        };

        this.getWeekTypeName = function () {
            return $.Enumerable
                .From(pageModel.WeekTypes)
                .Where(function (x) { return x.Id == self.weekType; })
                .FirstOrDefault({ Name: "<Тип недели не выбран>" })
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
            this.building = pageModel.CurrentBuildingId;
            this.weekType = pageModel.CurrentWeekTypeId;
            this.semestr = pageModel.CurrentSemesterId;

            this.onChanged();
        };
    }

    return new settings();
});