using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
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
