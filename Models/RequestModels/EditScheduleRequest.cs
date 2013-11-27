using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Site.Models.RequestModels
{
    public class EditScheduleRequest: CreateScheduleRequest
    {
        public int Id { get; set; }
    }
}