using System.Runtime.Serialization;
using Timetable.Data.Models;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class AuditoriumTypeDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Pattern { get; set; }

        public AuditoriumTypeDataTransfer(AuditoriumType auditoriumType)
        {
            Name = auditoriumType.Name;
            Pattern = auditoriumType.Pattern;
            Id = auditoriumType.Id;
        }
    }
}
