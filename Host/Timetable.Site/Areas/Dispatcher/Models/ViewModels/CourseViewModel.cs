using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
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