app.directive('scheduleCard', function ($compile) {
    return {
        restrict: 'A',
        scope: {
            model: '=',
            onDrop: '&',
            onDragStart: '&',
            onDragStop: '&'
        },
        link: function (scope, element, attrs) {
            var html = '<td class="day-content full" ng-class="" ng-click="model.dayCellClick($event)" data-drop="true" data-jqyoui-options jqyoui-droppable="{onDrop: \'model.startPlaning(this, pair, 0)\'}">' +
                '<div class="schedule-card alert alert-success" data-drag="true" data-jqyoui-options="{containment: \'#test2\', revert: \'invalid\', distance: 20}" jqyoui-draggable="{animate:true, onStart: \'model.startDragging(scheduleInfo)\', onStop: \'model.stopDragging()\'}"></div>' +
            '</td>';
            var e = $compile(html)(scope);
            element.replaceWith(e);
        },
    };
});