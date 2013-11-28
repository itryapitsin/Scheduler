using System.Collections.Generic;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class SpecialityDataTransfer : BaseDataTransfer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
	    public string ShortName { get; set; }
        [DataMember]
	    public string Code { get; set; }

        public SpecialityDataTransfer() { }

        public SpecialityDataTransfer(Speciality speciality)
        {
            Id = speciality.Id;
            Name = speciality.Name;
            Code = speciality.Code;
        }
    }
}
