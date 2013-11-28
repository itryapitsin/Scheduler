using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class LecturerDataTransfer : BaseDataTransfer
    {
        
	    public string Lastname { get; set; }
        
	    public string Firstname { get; set; }
        
	    public string Middlename { get; set; }
        
	    public string Contacts { get; set; }
        
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
