using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Timetable.Data.Models.Scheduler
{
    public class TutorialType : BaseIIASEntity
    {
        public string Name { get; set; }
        public ICollection<Auditorium> AuditoriumApplicabilities { get; set; }

        public TutorialType()
        {
            AuditoriumApplicabilities = new Collection<Auditorium>();
        }
    }

    public enum Types
    {
        Lecture = 1,

        Practice = 2,

        Lab = 3
    }
}
