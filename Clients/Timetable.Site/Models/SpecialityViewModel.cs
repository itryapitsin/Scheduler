using Timetable.Site.NewDataService;

namespace Timetable.Site.Models
{
    public class SpecialityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SpecialityViewModel(Speciality speciality)
        {
            Id = speciality.Id;
            Name = string.Format("{0} {1}", speciality.Name, speciality.Code);
        }

        public SpecialityViewModel(SpecialityDataTransfer speciality)
        {
            Id = speciality.Id;
            Name = string.Format("{0} {1}", speciality.Name, speciality.Code);
        }
    }
}