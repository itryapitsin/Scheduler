using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class CourseDataTransfer : BaseDataTransfer
    {
        
		public string Name { get; set; }

        public CourseDataTransfer()
        {
        }
        public CourseDataTransfer(Course course)
        {
            Id = course.Id;
            Name = course.Name;
        }

	}
}



