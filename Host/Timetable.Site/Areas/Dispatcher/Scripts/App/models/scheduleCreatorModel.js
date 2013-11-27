app.factory('scheduleCreatorModel', function ($http, $resource, $modal, $q) {
    function planingModal() {
        var self = this;

        this.scheduleTypes = pageModel.ScheduleTypes;
        this.scheduleType = this.scheduleTypes[0].Id;
        this.building = pageModel.CurrentBuildingId;
        this.times = pageModel.Times;
        this.weekTypes = pageModel.WeekTypes;

        this.getFreeAuditoriums = function () {
            if (!self.building || !self.dayOfWeek || !self.weekType || !self.pair)
                return;

            var params = {
                buildingId: self.building,
                dayOfWeek: self.dayOfWeek,
                weekTypeId: self.weekType,
                pair: self.pair
            };

            $http
                .get($http.prefix + "Scheduler/GetFreeAuditoriums", { params: params })
                .success(function (response) {
                    self.auditoriums = response;
                    self.auditorium = null;
                });
        };

        this.pairChanged = function () {
            self.getFreeAuditoriums();
            self.getTimeForPair();
        };

        this.weekTypeChanged = function () {
            self.getFreeAuditoriums();
        };

        this.buildingChanged = function () {
            $http
                .get($http.prefix + "Scheduler/BuildingChanged", { params: { buildingId: self.building } })
                .success(function (response) {
                    self.times = response.Times;
                    self.auditoriums = response.Auditoriums;

                    self.time = $.Enumerable.From(self.times)
                        .Where(function (item) { return item.Position == self.pair; })
                        .FirstOrDefault();
                });
        };

        this.getTimeForPair = function() {
            self.time = $.Enumerable.From(self.times)
                .Where(function(item) { return item.Position == self.pair; })
                .FirstOrDefault();
        };

        this.show = function ($scope) {
            var modalPromise = $modal({
                template: $http.prefix + 'Scheduler/PlaningModal',
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

        this.apply = function () {
            $(self.ui).remove();

            var params = {
                auditoriumId: self.auditorium,
                dayOfWeek: self.dayOfWeek,
                scheduleInfoId: self.scheduleInfo.Id,
                timeId: self.time.Id,
                weekTypeId: self.weekType,
                typeId: self.scheduleType
            };

            $http
                .post($http.prefix + 'Scheduler/Create', params)
                .success(function (response) {
                    if (self.onApplied)
                        self.onApplied(response);

                    self.close();
                });
        };

        this.close = function () {
            $q.when(self.promise).then(function (modalEl) {
                modalEl.modal('hide');
                modalEl.addClass("hide");
            });

            $(self.ui.draggable[0])
                .removeClass('hide')
                .css("top", "")
                .css("left", "0");
        };
    }

    function model() {
        var self = this;

        this.schedules = pageModel.Schedules;
        
        this.modal = new planingModal();
        this.modal.onApplied = function(newSchedule) {
            self.schedules.push(newSchedule);
        };

        this.loadScheduleInfoesForFaculty = function (facultyId, courseId, groups, studyYearId, semestr) {
            var groupIds = $.Enumerable.From(groups).Select('$.Id').ToArray();
            var params = {
                facultyId: facultyId,
                courseId: courseId,
                groupIds: groupIds,
                studyYearId: studyYearId,
                semesterId: semestr
            };

            $http
            .get($http.prefix + "Scheduler/GetSchedulesAndInfoes", { params: params })
            .success(function (response) {
                self.scheduleInfoes = response.ScheduleInfoes;
                self.schedules = response.Schedules;
            });
        };

        this.edit = function (scope) {
            self.modal.show(scope);
        };

        this.delete = function() {

        };

        this.dayCellClick = function (e, item) {
            if (e.target.tagName == "TD") {
                $(".alert-info").removeClass("alert-info");
                self.selected = false;
                return;
            }

            var hasClass = $(e.target).hasClass("alert-info");

            $(".alert-info").removeClass("alert-info");
            self.selected = false;
            if (!hasClass) {
                $(e.target).addClass("alert-info");
                self.selected = true;
            }
        };

        this.findScheduleTickets = function (pair, dayOfWeek) {
            var result = $.Enumerable.From(self.schedules)
                .Where(function (x) {
                    return x.Pair == pair && x.DayOfWeek == dayOfWeek;
                })
                .ToArray();

            return result;
        };

        this.hasFullScheduleTicket = function (pair, dayOfWeek) {
            var result = $.Enumerable.From(self.schedules)
                .Where(function (x) {
                    return x.Pair == pair && x.DayOfWeek == dayOfWeek;
                })
                .ToArray();

            if (result.length == 1)
                return result[0].WeekTypeName == 'Л';
            return false;
        };

        this.hasEvenScheduleTicket = function (pair, dayOfWeek) {
            var result = $.Enumerable.From(self.schedules)
                .Where(function (x) {
                    return x.Pair == pair && x.DayOfWeek == dayOfWeek;
                })
                .ToArray();

            if (result.length == 1)
                return result[0].WeekTypeName == 'Ч';

            return result.length > 1;
        };

        this.hasOddScheduleTicket = function (pair, dayOfWeek) {
            var result = $.Enumerable.From(self.schedules)
                .Where(function (x) {
                    return x.Pair == pair && x.DayOfWeek == dayOfWeek;
                })
                .ToArray();

            if (result.length == 1)
                return result[0].WeekTypeName == 'З';

            return result.length > 1;
        };

        this.hasScheduleTicket = function (pair, dayOfWeek) {
            var result = self.findScheduleTicket(pair, dayOfWeek);
            return result.length > 0;
        };

        this.isFullScheduleTicket = function (schedule) {
            return schedule.WeekTypeName == 'Л';
        };

        this.isEvenScheduleTicket = function (schedule) {
            return schedule.WeekTypeName == 'Ч';
        };

        this.isOddScheduleTicket = function (schedule) {
            return schedule.WeekTypeName == 'З';
        };

        this.startPlaning = function (e, ui, scope, pair, dayOfWeek) {
            self.modal.show(scope);
            self.modal.scheduleInfo = self.draggedItem;
            self.modal.pair = pair;
            self.modal.dayOfWeek = dayOfWeek;
            self.modal.ui = ui;
            self.modal.getTimeForPair();
            //self.modal.time = $.Enumerable.From(self.modal.times)
            //    .Where(function (item) { return item.Position == self.modal.pair; })
            //    .FirstOrDefault();

            $(ui.draggable[0]).addClass("hide");
        };

        this.stopDragging = function (e, ui) {
            $("#test1").css("overflow-y", "scroll");
        };

        this.startDragging = function (e, ui, item) {
            self.draggedItem = item;

            $("#test1").css("overflow", "");
        };
    }

    return new model();
});