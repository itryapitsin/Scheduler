﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.RequestModels
{
    public class CreateScheduleRequest
    {
        public int AuditoriumId { get; set; }
        public int DayOfWeek { get; set; }
        public int ScheduleInfoId { get; set; }
        public int TimeId { get; set; }
        public int WeekTypeId { get; set; }
        public int TypeId { get; set; }
    }
}