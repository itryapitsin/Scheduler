using System.Collections.Generic;

namespace Timetable.Data.Models.Scheduler
{
    public class AuditoriumEqualityComparer: IEqualityComparer<Auditorium>
    {
        public bool Equals(Auditorium x, Auditorium y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Auditorium obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
