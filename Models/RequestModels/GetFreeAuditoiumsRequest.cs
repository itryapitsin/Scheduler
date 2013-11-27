using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.RequestModels
{
    public class GetFreeAuditoiumsRequest
    {
        public int BuildingId { get; set; }
        public int DayOfWeek { get; set; }
        public int WeekTypeId { get; set; }
        public int Pair { get; set; }
    }
}