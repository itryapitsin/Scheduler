using System.Collections.Generic;
using System.Collections.ObjectModel;
using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    public class OrganizationDataTransfer : BaseDataTransfer
    {
        public string Name { get; set; }

        public OrganizationDataTransfer() {}

        public OrganizationDataTransfer(Organization organization)
        {
            Id = organization.Id;
            Name = organization.Name;
        }
    }
}
