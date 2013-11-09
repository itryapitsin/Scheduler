using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class OrganizationDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }

        public OrganizationDataTransfer() {}

        public OrganizationDataTransfer(Organization organization)
        {
            Id = organization.Id;
            Name = organization.Name;
        }
    }
}
