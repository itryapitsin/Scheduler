using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    public class Branch: BaseIIASEntity
    {
        public string Name { get; set; }
        public ICollection<Faculty> Faculties { get; set; }
        public virtual Organization Organization { get; set; }
        public int OrganizationId { get; set; }
        public Branch()
        {
            Faculties = new Collection<Faculty>();
        }
    }
}
