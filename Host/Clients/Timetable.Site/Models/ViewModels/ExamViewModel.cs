using Timetable.Logic.Models.Scheduler;

namespace Timetable.Site.Models.ViewModels
{
    public class ExamViewModel
    {
        public int Id { get; set; }

        public string LecturerName { get; set; }

        public int LecturerId { get; set; }

        public string GroupCode { get; set; }

        public int GroupId { get; set; }

        public string TutorialName { get; set; }

        public int TutorialId { get; set; }

        public string AuditoriumNumber { get; set; }

        public int AuditoriumId { get; set; }

        public string Time { get; set; }

        public ExamViewModel(ExamDataTransfer exam)
        {
            Id = exam.Id;
            if (exam.Lecturer != null)
            {
                LecturerName = LecturerViewModel.GetLecturerShortName(exam.Lecturer);
                LecturerId = exam.Lecturer.Id;
            }

            if (exam.Group != null)
            {
                GroupCode = exam.Group.Code;
                GroupId = exam.Group.Id;
            }

            if (exam.Tutorial != null)
            {
                TutorialName = exam.Tutorial.Name;
                TutorialId = exam.Tutorial.Id;
            }

            if (exam.Auditorium != null)
            {
                AuditoriumNumber = exam.Auditorium.Number;
                AuditoriumId = exam.Auditorium.Id;
            }

            Time = exam.Time.ToString("MM-dd-yyyy-hh:mm");
        }
    }
}