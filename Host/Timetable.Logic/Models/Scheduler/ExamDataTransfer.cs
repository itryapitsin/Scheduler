using System;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Logic.Models.Scheduler
{
    public class ExamDataTransfer : BaseDataTransfer
    {
        public DateTime Time { get; set; }

        public LecturerDataTransfer Lecturer { get; set; }

        public GroupDataTransfer Group { get; set; }

        public TutorialDataTransfer Tutorial { get; set; }

        public AuditoriumDataTransfer Auditorium { get; set; }

        public ExamDataTransfer() {}

        public ExamDataTransfer(Exam exam)
        {
            Time = exam.Time;
            Lecturer = new LecturerDataTransfer(exam.Lecturer);
            Group = new GroupDataTransfer(exam.Group);
            Tutorial = new TutorialDataTransfer(exam.Tutorial);
            Auditorium = new AuditoriumDataTransfer(exam.Auditorium);
        }
    }
}
