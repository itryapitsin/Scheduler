using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Data.IIAS.Models
{
    public class Group
    {
        public Int64 Id { get; set; }

        public string Code { get; set; }

        public Int64 CourseId { get; set; }

        public Int64 SpecialityId { get; set; }

        public Int64 FacultyId { get; set; }
    }
}
