using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    public class AuditoriumOrderDataTransfer : BaseDataTransfer
    {
        public string ThreadName { get; set; }

        public string LecturerName { get; set; }

        public string TutorialName { get; set; }

        public int DayOfWeek { get; set; }

        public TimeDataTransfer Time { get; set; }

        public AuditoriumDataTransfer Auditorium { get; set; }

        public bool AutoDelete { get; set; }

        public AuditoriumOrderDataTransfer() {}

        public AuditoriumOrderDataTransfer(AuditoriumOrder auditoriumOrder)
        {
            ThreadName = auditoriumOrder.ThreadName;
            LecturerName = auditoriumOrder.LecturerName;
            TutorialName = auditoriumOrder.TutorialName;
            DayOfWeek = auditoriumOrder.DayOfWeek;

            if (auditoriumOrder.Time != null)
                Time = new TimeDataTransfer(auditoriumOrder.Time);

            if(auditoriumOrder.Auditorium != null)
                Auditorium = new AuditoriumDataTransfer(auditoriumOrder.Auditorium);

            AutoDelete = auditoriumOrder.AutoDelete;
        }
    }
}
