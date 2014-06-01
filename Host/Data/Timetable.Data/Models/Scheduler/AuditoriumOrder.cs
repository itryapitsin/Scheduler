using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Timetable.Data.Models.Scheduler
{
    public class AuditoriumOrder : BaseIIASEntity
    {
        public string TutorialName { get; set; }
        public string LecturerName { get; set; }
        public string ThreadName { get; set; }
        public virtual Time Time { get; set; }
        public int TimeId { get; set; }
        public DateTime Date { get; set; }
        public virtual Auditorium Auditorium { get; set; }
        public int AuditoriumId { get; set; }
        public bool AutoDelete { get; set; }
    }
}
