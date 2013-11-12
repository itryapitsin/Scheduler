using System.Collections.Generic;
using Timetable.Site.Models.Schedules;
using SendTime = Timetable.Site.Models.Times.SendModel;

namespace Timetable.Site.Models
{
    public class PrintScheduleInfoModel
    {
        public string Name { get; set; }
    }
    public class PrintScheduleModel
    {
        public ScheduleViewModel[,,] ScheduleTable { get; set; }
        public IEnumerable<SendTime> Times { get; set; }
        public string Header { get; set; }
        public IEnumerable<string> Days { get; set; }
        public int FontSize { get; set; }
        public string Mode { get; set; }
    }

    public class PrintScheduleForGroupsModel
    {
        public ScheduleViewModel[,,,] ScheduleTable { get; set; } //Group, Day, Time, Period
        public int[, ,] Colspan { get; set; }
        public bool[, ,] Skip2 { get; set; }
        public int[] Rowspan { get; set; }
        public bool[,] Skip { get; set; }
        public IEnumerable<SendTime> Times { get; set; }
        public string Header { get; set; }
        public IEnumerable<string> Days { get; set; }
        public IEnumerable<GroupViewModel> Groups { get; set; }
        public int FontSize { get; set; }
    }
}