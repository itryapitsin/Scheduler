using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Data.Models.Personalization;

namespace Timetable.Logic.Models
{
    public class CreatorSettings
    {
        public int? CurrentBuildingId { get; set; }
        public int? CurrentStudyYearId { get; set; }
        public int? CurrentStudyTypeId { get; set; }
        public int? CurrentSemesterId { get; set; }
        public int? CurrentBranchId { get; set; }
        public int? CurrentFacultyId { get; set; }
        public int? CurrentCourseId { get; set; }
        public int[] CurrentGroupIds { get; set; }

        public int? CurrentPlanningScheduleTypeId { get; set; }

        public int? CurrentPlanningBuildingId { get; set; }

        public int? CurrentPlanningAuditoriumId { get; set; }

        public int? CurrentPlanningWeekTypeId { get; set; }

        public string CurrentPlanningSubGroup { get; set; }

        public CreatorSettings(User user)
        {
            CurrentPlanningScheduleTypeId = user.PlanningModalSelectedScheduleTypeId;
            CurrentPlanningBuildingId = user.PlanningModalSelectedBuildingId;
            CurrentPlanningAuditoriumId = user.PlanningModalSelectedAuditoriumId;
            CurrentPlanningWeekTypeId = user.PlanningModalSelectedWeekTypeId;
            CurrentPlanningSubGroup = user.PlanningModalSelectedSubGroup;

            CurrentBuildingId = user.CreatorSelectedBuildingId;
            CurrentStudyYearId = user.CreatorSelectedStudyYearId;
            CurrentStudyTypeId = user.CreatorSelectedStudyTypeId;
            CurrentSemesterId = user.CreatorSelectedSemesterId;
            CurrentBranchId = user.CreatorSelectedBranchId;
            CurrentFacultyId = user.CreatorSelectedFacultyId;
            CurrentCourseId = user.CreatorSelectedCourseId;

            if (user.CreatorSelectedGroups != null)
                CurrentGroupIds = user.CreatorSelectedGroups.Select(x => x.Id).ToArray();
        }
    }
}
