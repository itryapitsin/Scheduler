using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Timetable.Data.Models.Scheduler
{
    public class Department: BaseEntity 
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public ICollection<Faculty> Faculties { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
        public Department()
        {
            Faculties = new List<Faculty>();
            Lecturers = new List<Lecturer>();
        }
    }
}
