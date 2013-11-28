using Timetable.Data.Models.Personalization;

namespace Timetable.Logic.Models
{
    public class LecturerScheduleSettings
    {
        public int? LecturerId { get; set; }
        public int? StudyYearId { get; set; }
        public int? SemesterId { get; set; }

        public LecturerScheduleSettings(User user)
        {
            LecturerId = user.LecturerScheduleSelectedLecturerId;
            StudyYearId = user.LecturerScheduleSelectedStudyYearId;
            SemesterId = user.LecturerScheduleSelectedSemesterId;
        }
    }
}
