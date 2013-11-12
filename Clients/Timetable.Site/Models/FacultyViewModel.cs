using Timetable.Site.NewDataService;

namespace Timetable.Site.Models
{
    public class FacultyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FacultyViewModel(Faculty branch)
        {
            Id = branch.Id;
            Name = branch.Name;
        }

        public FacultyViewModel(FacultyDataTransfer faculty)
        {
            Id = faculty.Id;
            Name = faculty.Name;
        }
    }
}