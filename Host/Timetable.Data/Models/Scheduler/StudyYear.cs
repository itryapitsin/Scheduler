using System.ComponentModel.DataAnnotations.Schema;

namespace Timetable.Data.Models.Scheduler
{
    public class StudyYear: BaseIIASEntity
    {
        public int StartYear { get; set; }

        public int Length { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int EndYear
        {
            get { return StartYear + Length; }
        }
    }
}
