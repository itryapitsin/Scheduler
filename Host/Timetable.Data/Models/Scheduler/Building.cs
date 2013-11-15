using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    public class Building : BaseIIASEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ShortName { get; set; }
        public string Info { get; set; }
        public virtual ICollection<Time> Times { get; set; }
        public Building()
        {
            Times = new Collection<Time>();
        }
    }
}
