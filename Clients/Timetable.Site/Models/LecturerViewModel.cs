using Timetable.Site.NewDataService;

namespace Timetable.Site.Models
{
    public class LecturerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LecturerViewModel(Lecturer lecturer)
        {
            if (lecturer.Lastname != null && lecturer.Firstname != null && lecturer.Middlename != null)
                Name = lecturer.Lastname + " " + lecturer.Firstname[0] + ". " + lecturer.Middlename[0] + ".";
        }

        public LecturerViewModel(LecturerDataTransfer lecturer)
        {
            if (lecturer.Lastname != null && lecturer.Firstname != null && lecturer.Middlename != null)
                Name = lecturer.Lastname + " " + lecturer.Firstname[0] + ". " + lecturer.Middlename[0] + ".";
        }
    }
}