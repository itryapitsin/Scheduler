using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Host.Models.Scheduler
{
    [DataContract]
    public class LecturerDataTransfer : BaseDataTransfer
    {
        [DataMember]
	    public string Lastname { get; set; }
        [DataMember]
	    public string Firstname { get; set; }
        [DataMember]
	    public string Middlename { get; set; }
        [DataMember]
	    public string Contacts { get; set; }
        [DataMember]
        public IEnumerable<PositionDataTransfer> Positions { get; set; }
        public LecturerDataTransfer()
        {
            Positions = new Collection<PositionDataTransfer>();
        }

        public LecturerDataTransfer(Lecturer lecturer)
        {
            Id = lecturer.Id;
            Lastname = lecturer.Lastname;
            Firstname = lecturer.Firstname;
            Middlename = lecturer.Middlename;
            Contacts = lecturer.Contacts;
            Positions = lecturer.Positions.Select(x => new PositionDataTransfer(x));
        }
    }
}
