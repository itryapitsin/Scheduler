using System;
using System.Collections.Generic;
using System.Linq;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Models;
using Timetable.Site.Infrastructure;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class AuditoriumScheduleOrderViewModel
    {
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<AuditoriumTypeViewModel> AuditoriumTypes { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }

        public IEnumerable<ScheduleViewModel> Schedules { get; set; }

        public IEnumerable<AuditoriumOrderViewModel> AuditoriumOrders { get; set; }
    
        public int? BuildingId { get; set; }
        public int? AuditoriumId { get; set; }
        public int? AuditoriumTypeId { get; set; }

        public int? TimeId { get; set; }

        public string Date { get; set; }

        public AuditoriumScheduleOrderViewModel(IDataService dataService, UserDataTransfer userData)
        {
            Buildings = dataService
                .GetBuildings()
                .Select(x => new BuildingViewModel(x));

            AuditoriumTypes = dataService
                .GetAuditoriumTypes(true)
                .Select(x => new AuditoriumTypeViewModel(x));

            if (userData.AuditoriumScheduleSettings.BuildingId.HasValue)
                Times = dataService
                    .GetTimes(userData.AuditoriumScheduleSettings.BuildingId.Value)
                    .Select(x => new TimeViewModel(x));

            BuildingId = userData.AuditoriumScheduleSettings.BuildingId;
            AuditoriumTypeId = userData.AuditoriumScheduleSettings.AuditoriumTypeIds.FirstOrDefault();
            TimeId = userData.AuditoriumScheduleSettings.TimeId;

            if (userData.AuditoriumScheduleSettings.Date.HasValue)
                Date = userData.AuditoriumScheduleSettings.Date.Value.ToString("yyyy-MM-dd");

            AuditoriumId = userData.AuditoriumScheduleSettings.AuditoriumId;

            if (!userData.AuditoriumScheduleSettings.BuildingId.HasValue
                || !userData.AuditoriumScheduleSettings.AuditoriumTypeIds.Any())
                return;

            Auditoriums = dataService
                .GetAuditoriums(
                    userData.AuditoriumScheduleSettings.BuildingId.Value,
                    userData.AuditoriumScheduleSettings.AuditoriumTypeIds.ToArray())
                .Select(x => new AuditoriumViewModel(x));

            var studyYear = dataService.GetStudyYear(DateTime.Now);
            var semester = dataService.GetSemesterForTime(DateTime.Now);

            var auditoriumIds = Auditoriums.Select(x => x.Id).ToArray();




            if (TimeId.HasValue && userData.AuditoriumScheduleSettings.Date.HasValue)
            {
                var dayOfWeek = (int)userData.AuditoriumScheduleSettings.Date.Value.DayOfWeek;

                if (dayOfWeek == 0)
                    dayOfWeek = 7;

                Schedules = dataService
                .GetSchedules(
                    auditoriumIds,
                    studyYear.Id,
                    semester.Id,
                    TimeId.Value,
                    dayOfWeek)
                .Select(x => new ScheduleViewModel(x));
            }


             if (userData.AuditoriumScheduleSettings.TimeId.HasValue
                && userData.AuditoriumScheduleSettings.Date.HasValue
                 )
             {

                 AuditoriumOrders = dataService.GetAuditoriumOrders(
                     userData.AuditoriumScheduleSettings.TimeId.Value,
                     userData.AuditoriumScheduleSettings.Date.Value.ToString("yyyy-MM-dd"),
                     userData.AuditoriumScheduleSettings.BuildingId.Value,
                     userData.AuditoriumScheduleSettings.AuditoriumTypeIds.ToArray()
                     ).Select(x => new AuditoriumOrderViewModel(x));
             }
        }
    }
}