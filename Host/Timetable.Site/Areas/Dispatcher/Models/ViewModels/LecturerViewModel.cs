using System;
using System.Linq;
using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Areas.Dispatcher.Models.ViewModels
{
    public class LecturerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LecturerViewModel(LecturerDataTransfer lecturer)
        {
            if (lecturer.Lastname != null && lecturer.Firstname != null && lecturer.Middlename != null)
                Name = lecturer.Lastname + " " + lecturer.Firstname[0] + ". " + lecturer.Middlename[0] + ".";

            Id = lecturer.Id;
        }

        public static string GetLecturerShortName(LecturerDataTransfer lecturer)
        {
            if (lecturer == null)
                return String.Empty;

            var shortName = lecturer.Lastname;

            if (lecturer.Firstname != null)
                shortName += " " + lecturer.Firstname.FirstOrDefault() + ".";

            if (lecturer.Middlename != null)
                shortName += lecturer.Middlename.FirstOrDefault() + ".";

            return shortName;
        }
    }
}