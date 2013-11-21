namespace Timetable.Site.Models
{
    public class GroupAddViewModel
    {
        public string Code { get; set; }
        public int StudentsCount { get; set; }
        public int CourseId { get; set; }
        public int ParentId { get; set; }
        public int SpecialityId { get; set; }
    }
}