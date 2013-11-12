using Timetable.Site.NewDataService;

namespace Timetable.Site.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CourseViewModel(CourseDataTransfer course)
        {
            Id = course.Id;
            Name = course.Name;
        }
    }
}