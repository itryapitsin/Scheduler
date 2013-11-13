﻿using System.Linq;
using System.Web.Mvc;
using Timetable.Site.Models;

namespace Timetable.Site.Controllers
{
    public class SchedulerController: NewBaseController
    {
        public PartialViewResult Index(int buildingId = 11)
        {
            var model = new SchedulerViewModel
            {
                Times = DataService
                    .GetTimes(buildingId)
                    .Select(x => new TimeViewModel(x)),

                Buildings = DataService
                    .GetBuildings()
                    .Select(x => new BuildingViewModel(x)),

                Branches = DataService
                    .GetBranches()
                    .Select(x => new BranchViewModel(x)),

                WeekTypes = DataService
                    .GetWeekTypes()
                    .Select(x => new WeekTypeViewModel(x)),

                Courses = DataService
                    .GetCources()
                    .OrderBy(x => x.Name)
                    .Select(x => new CourseViewModel(x)),

                StudyYears = DataService
                    .GetStudyYears()
                    .Select(x => new StudyYearViewModel(x))
            };

            return PartialView("_Index", model);
        }

        public PartialViewResult TimetableSettingsModal()
        {
            return PartialView("_TimetableSettings.Modal");
        }

        public PartialViewResult ThreadModal()
        {
            return PartialView("_Thread.Modal");
        }
    }
}