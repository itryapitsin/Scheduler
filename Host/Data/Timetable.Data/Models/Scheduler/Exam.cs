using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    public class Exam : BaseIIASEntity
    {
        public virtual Lecturer Lecturer { get; set; }

        public int LecturerId { get; set; }

        public virtual Group Group { get; set; }
        public int GroupId { get; set; }

        public virtual Tutorial Tutorial { get; set; }
        public int TutorialId { get; set; }

        public virtual Auditorium Auditorium { get; set; }
        public int? AuditoriumId { get; set; }

        public DateTime Time { get; set; }

    }
}
