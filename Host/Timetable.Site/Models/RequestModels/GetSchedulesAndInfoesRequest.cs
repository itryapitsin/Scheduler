using System;

namespace Timetable.Site.Models.RequestModels
{
    public class GetSchedulesAndInfoesRequest: GetScheduleBaseRequest
    {
        public int FacultyId { get; set; }
        public int CourseId { get; set; }
        public string GroupIds { get; set; }

        public int SpecialityId { get; set; }

        public bool IsForGroups()
        {
            return !String.IsNullOrEmpty(GroupIds);
        }

        public bool IsForSpeciality()
        {
            return SpecialityId != 0;
        }

        public bool IsForFaculty()
        {
            return FacultyId != 0;
        }
    }
}