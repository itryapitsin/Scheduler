using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class BuildingDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string Info { get; set; }
        public BuildingDataTransfer(Building building)
        {
            Id = building.Id;
            Name = building.Name;
            Address = building.Address;
            ShortName = building.ShortName;
            Info = building.Info;
        }
    }
}
