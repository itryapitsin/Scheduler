using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.Tutorials
{
    public class TutorialAddViewModel
    {
        public int FacultyId { get; set; }
        public int SpecialityId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}