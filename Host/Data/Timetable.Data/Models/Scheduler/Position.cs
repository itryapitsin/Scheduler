using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    public class Position: BaseIIASEntity
    {
        public string Name { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
        public Position()
        {
            Lecturers = new Collection<Lecturer>();
        }
    }
}
