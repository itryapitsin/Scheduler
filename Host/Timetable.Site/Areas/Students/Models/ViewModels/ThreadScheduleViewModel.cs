using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Timetable.Logic.Interfaces;
using Timetable.Site.Models.ViewModels;

namespace Timetable.Site.Areas.Students.Models.ViewModels
{
    public class ThreadScheduleViewModel
    {
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<FacultyViewModel> Faculties { get; set; }
        public IEnumerable<StudyTypeViewModel> StudyTypes { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
        public IEnumerable<GroupViewModel> Groups { get; set; }
        public IEnumerable<ScheduleViewModel> Schedules { get; set; }

        public int? BranchId { get; set; }
        public int? StudyFormId { get; set; }
        public int? FacultyId { get; set; }
        public int? CourseId { get; set; }
        public int? GroupId { get; set; }

        public ThreadScheduleViewModel() { }

        public ThreadScheduleViewModel(HttpRequestBase request, IDataService dataService)
        {
            var currentBranchId = request.Cookies["currentBranchId"];
            var currentStudyFormId = request.Cookies["currentStudyFormId"];
            var currentFacultyId = request.Cookies["currentFacultyId"];
            var currentCourseId = request.Cookies["currentCourseId"];
            var currentGroupId = request.Cookies["currentGroupId"];

            Branches = dataService
                .GetBranches()
                .Select(x => new BranchViewModel(x));

            StudyTypes = dataService
                .GetStudyType()
                .Select(x => new StudyTypeViewModel(x));

            if (currentBranchId != null)
            {
                BranchId = Convert.ToInt32(currentBranchId.Value);
                Faculties = dataService
                    .GetFaculties(BranchId.Value)
                    .Select(x => new FacultyViewModel(x));

                Courses = dataService
                    .GetCources(BranchId.Value)
                    .Select(x => new CourseViewModel(x));
            }

            if (currentStudyFormId != null)
            {
                StudyFormId = Convert.ToInt32(currentStudyFormId.Value);
            }

            if (currentFacultyId != null)
                FacultyId = Convert.ToInt32(currentFacultyId.Value);

            if (currentCourseId != null)
                CourseId = Convert.ToInt32(currentCourseId.Value);

            if (currentGroupId != null)
                GroupId = Convert.ToInt32(currentGroupId.Value);

            if (FacultyId.HasValue && CourseId.HasValue)
            {
                Groups = dataService
                    .GetGroupsForFaculty(FacultyId.Value, CourseId.Value)
                    .Select(x => new GroupViewModel(x));
            }
        }
    }
}