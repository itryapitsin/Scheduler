using System.Collections.Generic;

namespace Timetable.Site.Models.ResponseModels
{
    public class CollectionResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
    }
}