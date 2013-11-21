

using Timetable.Data.Models.Scheduler;

namespace Timetable.Site.Models
{
    public class LecturerAddViewModel
    {
        public string DepartmentIds { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Contacts { get; set; }
        public string PositionIds { get; set; }

        public Lecturer ToLecturer()
        {
            return new Lecturer
            {
                Contacts = Contacts,
                Firstname = Firstname,
                Middlename = Middlename,
                Lastname = Lastname,
            };
        }
    }
}