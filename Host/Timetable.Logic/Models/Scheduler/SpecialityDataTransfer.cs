using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    
    public class SpecialityDataTransfer : BaseDataTransfer
    {
        
        public string Name { get; set; }
        
	    public string ShortName { get; set; }
        
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
