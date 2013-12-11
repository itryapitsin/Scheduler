using System.Collections.Generic;
using System.Linq;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Models;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class AuditoriumScheduleGeneralViewModel
    {
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<AuditoriumTypeViewModel> AuditoriumTypes { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public IEnumerable<StudyYearViewModel> StudyYears { get; set; }
        public IEnumerable<SemesterViewModel> Semesters { get; set; }
        public IEnumerable<WeekTypeViewModel> WeekTypes { get; set; }
        public int? BuildingId { get; set; }
        public int? AuditoriumId { get; set; }
        public int? AuditoriumTypeId { get; set; }
        public int? StudyYearId { get; set; }
        public int? Semester { get; set; }

        public AuditoriumScheduleGeneralViewModel(IDataService dataService, UserDataTransfer userData)
        {
            Buildings = dataService
                .GetBuildings()
                .Select(x => new BuildingViewModel(x));

            StudyYears = dataService
                .GetStudyYears()
                .Select(x => new StudyYearViewModel(x));

            Semesters = dataService
                .GetSemesters()
                .Select(x => new SemesterViewModel(x));

            WeekTypes = dataService
                .GetWeekTypes()
                .Select(x => new WeekTypeViewModel(x));

            AuditoriumTypes = dataService
                .GetAuditoriumTypes(true)
                .Select(x => new AuditoriumTypeViewModel(x));

            BuildingId = userData.AuditoriumScheduleSettings.BuildingId;

            StudyYearId = userData.AuditoriumScheduleSettings.StudyYearId;

            Semester = userData.AuditoriumScheduleSettings.SemesterId;

            AuditoriumTypeId = userData.AuditoriumScheduleSettings.AuditoriumTypeIds.FirstOrDefault();

            if (userData.AuditoriumScheduleSettings.BuildingId.HasValue)
                Times = dataService
                    .GetTimes(userData.AuditoriumScheduleSettings.BuildingId.Value)
                    .Select(x => new TimeViewModel(x));

            if (!userData.AuditoriumScheduleSettings.BuildingId.HasValue
                || !userData.AuditoriumScheduleSettings.AuditoriumTypeIds.Any() 
                || !userData.AuditoriumScheduleSettings.SemesterId.HasValue 
                || !userData.AuditoriumScheduleSettings.StudyYearId.HasValue) 
                return;

            Auditoriums = dataService
                .GetAuditoriums(
                    userData.AuditoriumScheduleSettings.BuildingId.Value,
                    userData.AuditoriumScheduleSettings.AuditoriumTypeIds.ToArray())
                .Select(x => new AuditoriumViewModel(x));

            Schedules = dataService
                .GetSchedules(
                    Auditoriums.Select(x => x.Id).ToArray(),
                    userData.AuditoriumScheduleSettings.StudyYearId.Value,
                    userData.AuditoriumScheduleSettings.SemesterId.Value)
                .Select(x => new ScheduleViewModel(x));
        }
    }
}