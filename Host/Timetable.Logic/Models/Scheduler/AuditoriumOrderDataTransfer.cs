using Timetable.Data.Models.Scheduler;
using System;

namespace Timetable.Logic.Models.Scheduler
{
    public class AuditoriumOrderDataTransfer : BaseDataTransfer
    {
        public string ThreadName { get; set; }

        public string LecturerName { get; set; }

        public string TutorialName { get; set; }

        public TimeDataTransfer Time { get; set; }

        public AuditoriumDataTransfer Auditorium { get; set; }

        public bool AutoDelete { get; set; }

        public DateTime Date { get; set; }

        public AuditoriumOrderDataTransfer() {}

        public AuditoriumOrderDataTransfer(AuditoriumOrder auditoriumOrder)
        {
            Id = auditoriumOrder.Id;
            ThreadName = auditoriumOrder.ThreadName;
            LecturerName = auditoriumOrder.LecturerName;
            TutorialName = auditoriumOrder.TutorialName;
         
            Date = auditoriumOrder.Date;

            if (auditoriumOrder.Time != null)
                Time = new TimeDataTransfer(auditoriumOrder.Time);

            if(auditoriumOrder.Auditorium != null)
                Auditorium = new AuditoriumDataTransfer(auditoriumOrder.Auditorium);

            AutoDelete = auditoriumOrder.AutoDelete;
        }
    }
}
