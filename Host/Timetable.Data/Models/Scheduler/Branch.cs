﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    [DataContract(IsReference = true)]
    public class Branch: BaseEntity
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Faculties")]
        public ICollection<Faculty> Faculties { get; set; }

        public virtual Organization Organization { get; set; }

        public int OrganizationId { get; set; }

        public Branch()
        {
            Faculties = new Collection<Faculty>();
        }
    }
}