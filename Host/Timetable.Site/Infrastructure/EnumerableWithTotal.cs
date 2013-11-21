using System.Collections.Generic;

namespace Timetable.Site.Infrastructure
{
    public class EnumerableWithTotal<T>
    {
        public IList<T> Items { get; set; }

        public int Total { get; set; }
    }
}