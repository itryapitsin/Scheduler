using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Data.IIAS.Models
{
    public class ScheduleInfo
    {
        public Int64 Id { get; set; }

        public decimal HoursPerWeek { get; set; }

        public int LecturerId { get; set; }

        public int TutorialTypeId { get; set; }

        public int TutorialId { get; set; }

        public int DepartmentId { get; set; }

        public string StudyYear { get; set; }

        public int Semestr { get; set; }
    }
}
