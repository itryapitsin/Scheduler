using System.Runtime.Serialization;
using Timetable.Data.Models;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class BaseDataTransfer
    {
        [DataMember]
        public int Id { get; set; }

    }
}