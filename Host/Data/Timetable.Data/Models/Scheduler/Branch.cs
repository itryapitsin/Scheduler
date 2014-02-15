using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Timetable.Data.Models.Scheduler
{
    public class Branch: BaseIIASEntity
    {
        public string ShortName { get; set; }
        public string Name { get; set; }
        public ICollection<Faculty> Faculties { get; set; }
        public virtual Organization Organization { get; set; }
        public int OrganizationId { get; set; }
        public ICollection<Course> Courses { get; set; }
        public Branch()
        {
            Faculties = new Collection<Faculty>();
            Courses = new Collection<Course>();
        }
    }
}
