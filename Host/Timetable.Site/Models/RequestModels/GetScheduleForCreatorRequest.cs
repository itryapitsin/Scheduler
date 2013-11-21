namespace Timetable.Site.Models.RequestModels
{
    public class GetScheduleForCreatorRequest: GetScheduleBaseRequest
    {
        public int FacultyId { get; set; }
        public int CourseId { get; set; }
        public int[] GroupIds { get; set; }
        public int SpecialityId { get; set; }

        public bool IsForGroups()
        {
            return GroupIds != null && GroupIds.Length > 0;
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