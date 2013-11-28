using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class CourseDataTransfer : BaseDataTransfer
    {
        [DataMember]
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



