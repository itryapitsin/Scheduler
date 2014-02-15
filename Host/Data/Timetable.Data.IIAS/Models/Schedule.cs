using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable.Data.IIAS.Models
{
    public class Schedule
    {
        public Int64 Id { get; set; }
        public string WeekType { get; set; }
        public Int64 ScheduleInfoId { get; set; }
        public Int64 AuditoriumId { get; set; }
        public Int64? ScheduleTypeId { get; set; }
        public Int64 TimeId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int DayOfWeek { get; set; }
        public string SubGroup { get; set; }
    }
}
