using System.Collections.Generic;
using System.Linq;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Models;
using Timetable.Site.Models;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class SchedulerViewModel : BaseClientResponseModel
    {
        public IEnumerable<int> Pairs { get; set; }
        public IEnumerable<TimeViewModel> Times { get; set; }
        public IEnumerable<BuildingViewModel> Buildings { get; set; }
        public IEnumerable<AuditoriumViewModel> Auditoriums { get; set; }
        public IEnumerable<WeekTypeViewModel> WeekTypes { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<FacultyViewModel> Faculties { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
        public IEnumerable<GroupViewModel> Groups { get; set; }
        public IEnumerable<StudyYearViewModel> StudyYears { get; set; }
        public IEnumerable<StudyTypeViewModel> StudyTypes { get; set; }
        public IEnumerable<ScheduleTypeViewModel> ScheduleTypes { get; set; }
        public IEnumerable<SemesterViewModel> Semesters { get; set; }
        public IEnumerable<ScheduleInfoViewModel> ScheduleInfoes { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
        public int? CurrentBuildingId { get; set; }
        public int? CurrentStudyYearId { get; set; }
        public int? CurrentStudyTypeId { get; set; }
        public int? CurrentSemesterId { get; set; }
        public int? CurrentBranchId { get; set; }
        public int? CurrentFacultyId { get; set; }
        public int? CurrentCourseId { get; set; }
        public int[] CurrentGroupIds { get; set; }

        public SchedulerViewModel(
            IDataService dataService,
            UserDataTransfer userData)
            : base(dataService)
        {
            Buildings = dataService
                .GetBuildings()
                .Select(x => new BuildingViewModel(x));

            Branches = dataService
                .GetBranches()
                .Select(x => new BranchViewModel(x));

            WeekTypes = dataService
                .GetWeekTypes()
                .Select(x => new WeekTypeViewModel(x));
            
            StudyTypes = dataService
                .GetStudyTypes()
                .Select(x => new StudyTypeViewModel(x));

            StudyYears = dataService
                .GetStudyYears()
                .Select(x => new StudyYearViewModel(x));

            ScheduleTypes = dataService
                .GetScheduleTypes()
                .Select(x => new ScheduleTypeViewModel(x));

            Semesters = dataService
                .GetSemesters()
                .Select(x => new SemesterViewModel(x));

            Pairs = dataService.GetPairs();

            if (userData.CreatorSettings.CurrentBuildingId.HasValue)
            {
                Auditoriums = dataService
                    .GetAuditoriums(userData.CreatorSettings.CurrentBuildingId.Value)
                    .Select(x => new AuditoriumViewModel(x));

                Times = dataService
                    .GetTimes(userData.CreatorSettings.CurrentBuildingId.Value)
                    .Select(x => new TimeViewModel(x));
            }
            CurrentBuildingId = userData.CreatorSettings.CurrentBuildingId;
            CurrentStudyYearId = userData.CreatorSettings.CurrentStudyYearId;
            CurrentStudyTypeId = userData.CreatorSettings.CurrentStudyTypeId;
            CurrentSemesterId = userData.CreatorSettings.CurrentSemesterId;

            if (!userData.CreatorSettings.CurrentBranchId.HasValue)
                return;

            Courses = dataService
                .GetCources(userData.CreatorSettings.CurrentBranchId.Value)
                .OrderBy(x => x.Name)
                .Select(x => new CourseViewModel(x));
            
            Faculties = dataService
                .GetFaculties(userData.CreatorSettings.CurrentBranchId.Value)
                .Select(x => new FacultyViewModel(x));

            CurrentBranchId = userData.CreatorSettings.CurrentBranchId;
            CurrentFacultyId = userData.CreatorSettings.CurrentFacultyId;
            CurrentCourseId = userData.CreatorSettings.CurrentCourseId;

            CurrentGroupIds = userData.CreatorSettings.CurrentGroupIds;

            if (userData.CreatorSettings.CurrentFacultyId.HasValue
                && userData.CreatorSettings.CurrentCourseId.HasValue
                && CurrentStudyTypeId.HasValue)
            {
                Groups = dataService
                    .GetGroupsForFaculty(
                        userData.CreatorSettings.CurrentFacultyId.Value,
                        userData.CreatorSettings.CurrentCourseId.Value,
                        CurrentStudyTypeId.Value)
                    .Select(x => new GroupViewModel(x));
            }

            if (userData.CreatorSettings.CurrentFacultyId == null
                || userData.CreatorSettings.CurrentCourseId == null
                || userData.CreatorSettings.CurrentStudyYearId == null
                || userData.CreatorSettings.CurrentSemesterId == null)
                return;

            ScheduleInfoes = dataService
                .GetScheduleInfoes(
                    userData.CreatorSettings.CurrentFacultyId.Value,
                    userData.CreatorSettings.CurrentCourseId.Value,
                    userData.CreatorSettings.CurrentGroupIds,
                    userData.CreatorSettings.CurrentStudyYearId.Value,
                    userData.CreatorSettings.CurrentSemesterId.Value)
                .Select(x => new ScheduleInfoViewModel(x));

            Schedules = dataService
                .GetSchedules(
                    userData.CreatorSettings.CurrentFacultyId.Value,
                    userData.CreatorSettings.CurrentCourseId.Value,
                    userData.CreatorSettings.CurrentGroupIds,
                    userData.CreatorSettings.CurrentStudyYearId.Value,
                    userData.CreatorSettings.CurrentSemesterId.Value)
                .Select(x => new ScheduleViewModel(x));
        }
    }
}