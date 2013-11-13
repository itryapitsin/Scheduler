app.factory('settingsModel', function (localStorageService, $modal, $q, $http) {

    function modal(savedSettings) {
        var self = this;
        this.saved = false;
        this.studyYears = pageModel.StudyYears;
        this.buildings = pageModel.Buildings;
        this.weekTypes = pageModel.WeekTypes;
        this.semestrs = [
            { Id: 1, Name: "Первый семестр" },
            { Id: 2, Name: "Второй семестр" },
            { Id: 3, Name: "Третий семестр" },
            { Id: 4, Name: "Четвертый семестр" }];

        if (savedSettings && savedSettings.modal) {
            this.studyYear = savedSettings.modal.studyYear;
            this.building = savedSettings.modal.building;
            this.weekType = savedSettings.modal.weekType;
            this.semestr = savedSettings.modal.semestr;
        }

        this.isValid = function () {
            return this.studyYear
                && this.building
                && this.weekType
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
    }

    function settings(savedSettings) {
        var self = this;
        this.modal = new modal(savedSettings);
        this.modal.apply = function () {
            if (!this.isValid())
                return;

            this.close();

            if (self.modal && self.modal.building)
                self.building = self.modal.building;
            else
                self.building = { Name: "<Корпус не выбран>" };

            if (self.modal && self.modal.weekType)
                self.weekType = self.modal.weekType;
            else
                self.weekType = { Name: "<Тип недели не выбран>" };

            if (self.modal && self.modal.studyYear)
                self.studyYear = self.modal.studyYear;
            else
                self.studyYear = { Name: "<Учебный год не выбран>" };

            if (self.modal && self.modal.semestr)
                self.semestr = self.modal.semestr;
            else
                self.semestr = { Name: "<Семестр не выбран>" };

            self.onChanged();
        };
        this.changedEvent = null;
        this.onChanged = function () {
            if (this.changedEvent)
                this.changedEvent(this);

            localStorageService.add("settings", this);
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

        if (savedSettings) {
            if (savedSettings.building)
                this.building = savedSettings.building;

            if (savedSettings.weekType)
                this.weekType = savedSettings.weekType;

            if (savedSettings.studyYear)
                this.studyYear = savedSettings.studyYear;

            if (savedSettings.semestr)
                this.semestr = savedSettings.semestr;
        }
    }

    return new settings(
        localStorageService.get("settings"));
});